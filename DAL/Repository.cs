using Dapper;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.DAL
{
    public class Repository : IRepository
    {
        string cncStr;
        const string initLinksQuery = "SELECT * FROM Links";
        const string initUsersQuery = "SELECT * FROM Users";
        const string addLinkQuery = "AddShortLink";
        const string addUserQuery = "AddUser";
        const string checkUserPassQuery = "CheckUserPass";

        QueryResult queryResult;
        ArrayList dataSaved;
        string queryString;

        public IDataCollection<LinkModel> Links { get; protected set; }

        public IDataCollection<UserBaseModel> Users { get; protected set; }

        public Repository(IConfiguration configuration)
        {
            cncStr = configuration.GetConnectionString("DefaultConnection");
            INIT_QUERY_VARS(null); 
            Links = LOAD_DATA<LinkModel>(initLinksQuery);
            Users = LOAD_DATA<UserBaseModel>(initUsersQuery);
        }

        public QueryResult SaveChanges()
        {
            INIT_QUERY_VARS(addLinkQuery);
            AddData(Links);
            queryString = addUserQuery;
            AddData(Users);
            queryResult.Data = dataSaved;
            return queryResult;
        }

        public bool CheckUserPass(UserBaseModel user)
        {
            INIT_QUERY_VARS(checkUserPassQuery);
            CallDBProcedure(user);
            var checkedUser = dataSaved[0];
            return checkedUser != null;
        }

        IDataCollection<T> LOAD_DATA<T>(string queryString)
        {
            var connection = new MySqlConnection(cncStr);
            IQueryable<T> initialData = null;
            TryProcessData<LinkModel>(connection, () =>
            {
                initialData = connection.Query<T>(queryString).AsQueryable();
            });
            if (!queryResult.IsSuccessful) throw new Exception(string.Join("; ", queryResult.Errors));
            return new DataCollection<T>(initialData);
        }

        void INIT_QUERY_VARS(string queryString)
        {
            queryResult = new QueryResult();
            dataSaved = new ArrayList();
            this.queryString = queryString;
        }

        protected void AddData<T>(IDataCollection<T> items)
        {
            items.SaveAdded(CallDBProcedure);
        }

        bool CallDBProcedure<T>(T item)
        {
            var connection = new MySqlConnection(cncStr);
            TryProcessData<T>(connection, () =>
            {
                item = connection.Query<T>(queryString, commandType: CommandType.StoredProcedure,
                    param: item).FirstOrDefault();
                if (item != null) dataSaved.Add(item);
            });
            return queryResult.IsSuccessful && item != null;
        }

        void TryProcessData<T>(IDbConnection connection, Action action)
        {
            try
            {
                connection.Open();
                action();
            }
            catch (Exception e)
            {
                if (queryResult.Errors == null) queryResult.Errors = e.Message.Split("\r\n");
                else queryResult.Errors.Concat(e.Message.Split("\r\n"));
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
