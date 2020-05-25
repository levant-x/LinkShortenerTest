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

        public IDataCollection<UserModel> Users => throw new NotImplementedException();

        public TestRepository()
        {

            Links = new DataCollection<LinkModel>(new LinkModel[]
            {
                new LinkModel() { ShortURL = "123", URL = "https://www.yandex.ru" },
                new LinkModel() { ShortURL = "456", URL = "https://www.google.com" }
            }.AsQueryable());
        }

        public DataQueryResult SaveChanges()
        {
            return new DataQueryResult();
        }
    }
}
