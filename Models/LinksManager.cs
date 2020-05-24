using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class LinksManager
    {
        IRepository repository;

        public LinksManager(IRepository repository)
        {
            this.repository = repository;
        }

        public DataQueryResult ShortenURL(string url)
        {
            if (repository == null) return null;
            var shortURL = string.Empty;
            do
            {
                shortURL = Helpers.GetAlphaNumString(Helpers.ShortURLLength);
            } while (repository.Links.FirstOrDefault(l => l.ShortURL == shortURL) != null);

            var newLink = new Link() { URL = url, ShortURL = shortURL };
            repository.Links.Append(newLink);
            var res = repository.SaveChanges();
            if (res.IsSuccessful) res.Data = newLink;
            return res;
        }        
    }
}
