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
        const string addLinkQuery = "AddShortLink";

        QueryResult queryResult;
        string queryString;
        ArrayList dataSaved;

        public IDataCollection<LinkModel> Links { get; protected set; }

        public IDataCollection<UserBaseModel> Users => throw new NotImplementedException();

        public Repository(IConfiguration configuration)
        {
            cncStr = configuration.GetConnectionString("DefaultConnection");
            queryResult = new QueryResult();
            LoadData();
        }

        public QueryResult SaveChanges()
        {
            queryResult = new QueryResult();
            dataSaved = new ArrayList();
            queryString = addLinkQuery;
            AddData(Links);
            queryResult.Data = dataSaved;
            return queryResult;
        }

        void LoadData()
        {
            var connection = new MySqlConnection(cncStr);
            TryProcessData<LinkModel>(connection, () =>
            {
                var dataQuery = connection.Query<LinkModel>(initLinksQuery).AsQueryable();
                Links = new DataCollection<LinkModel>(dataQuery);
            });
            if (!queryResult.IsSuccessful) throw new Exception(string.Join("; ", queryResult.Errors));
        }

        protected void AddData<T>(IDataCollection<T> items)
        {
            items.SaveAdded(SaveDataItemIntoDB);
        }

        bool SaveDataItemIntoDB<T>(T item)
        {
            var connection = new MySqlConnection(cncStr);
            TryProcessData<LinkModel>(connection, () =>
            {
                item = connection.Query<T>(queryString, commandType: CommandType.StoredProcedure,
                    param: item).First();
                dataSaved.Add(item);
            });
            return queryResult.IsSuccessful;
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
