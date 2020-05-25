using Dapper;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using MySql.Data.MySqlClient;
using System;
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
        DataQueryResult transactionResult = null;

        public IDataCollection<LinkModel> Links { get; protected set; }

        public IDataCollection<UserModel> Users => throw new NotImplementedException();

        public Repository(IConfiguration configuration)
        {
            cncStr = configuration.GetConnectionString("DefaultConnection");
            LoadData();
        }

        public DataQueryResult SaveChanges()
        {
            var res = AddData(Links);
            return res;
        }

        void LoadData()
        {
            var connection = new MySqlConnection(cncStr);
            var res = TryProcessData<LinkModel>(connection, () =>
            {
                var dataQuery = connection.Query<LinkModel>(initLinksQuery).AsQueryable();
                Links = new DataCollection<LinkModel>(dataQuery);
            });
            if (!res.IsSuccessful) throw new Exception(string.Join("; ", res.Errors));
        }

        protected DataQueryResult AddData<T>(IDataCollection<T> items)
        {
            items.SaveAdded(SaveDataItemIntoDB);
            return transactionResult;
        }

        bool SaveDataItemIntoDB<T>(T item)
        {
            var connection = new MySqlConnection(cncStr);
            transactionResult = TryProcessData<LinkModel>(connection, () =>
            {
                item = connection.Query<T>(addLinkQuery, commandType: CommandType.StoredProcedure,
                    param: item).First();
            });
            return transactionResult.IsSuccessful;
        }

        DataQueryResult TryProcessData<T>(IDbConnection connection, Action action)
        {
            var res = new DataQueryResult();
            try
            {
                connection.Open();
                action();
            }
            catch (Exception e)
            {
                res.Errors = e.Message.Split("\r\n");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return res;
        }
    }
}
