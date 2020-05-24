using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.DAL
{
    public class TestRepository : IRepository
    {
        List<Link> links;
        public IQueryable<Link> Links { get; }


        public TestRepository()
        {
            Links = new List<Link>()
            {
                new Link() { ShortURL = "123", URL = "https://www.yandex.ru" },
                new Link() { ShortURL = "456", URL = "https://www.google.com" }
            }
            .AsQueryable();
        }

        public DataQueryResult SaveChanges()
        {
            return new DataQueryResult();
        }
    }
}
