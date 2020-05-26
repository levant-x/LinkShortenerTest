using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.DAL
{
    public class TestRepository : IRepository
    {
        public IDataCollection<LinkModel> Links { get; private set; }

        public IDataCollection<UserBaseModel> Users { get; private set; }

        public TestRepository()
        {
            Links = new DataCollection<LinkModel>(new LinkModel[]
            {
                new LinkModel() { ShortURL = "123", URL = "https://www.yandex.ru" },
                new LinkModel() { ShortURL = "456", URL = "https://www.google.com" }
            }.AsQueryable());

            Users = new DataCollection<UserBaseModel>(new UserBaseModel[]
             {
                new UserBaseModel() { Name = "admin1", Password = "0000" },
                new UserBaseModel() { Name = "admin2", Password = "0000" }
             }.AsQueryable());
        }

        public bool CheckUserPass(string password)
        {
            return Users.FirstOrDefault(u => u.Password == password) != null;
        }


        public QueryResult SaveChanges()
        {
            Users.SaveAdded(u => { Users.Append(u); return true; });
            return new QueryResult();
        }
    }
}
