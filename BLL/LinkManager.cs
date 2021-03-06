﻿using LinkShortener.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.BLL
{
    public class LinkManager : ManagerBase
    {
        public LinkManager(IRepository repository) : base(repository)
        {

        }


        public QueryResult ShortenURL(string url, string hostName, int userID)
        {
            var shortURL = string.Empty;
            do
            {
                shortURL = Helpers.GetAlphaNumString(Helpers.ShortURLLength);
            } while (repository.Links.FirstOrDefault(l => l.ShortURL == shortURL) != null);

            var newLink = new LinkModel()
            {
                URL = url,
                ShortURL = $"{hostName}/{shortURL}",
                UserID = userID
            };
            repository.Links.AddItem(newLink);
            var res = repository.SaveChanges();
            return res;
        }  
    }
}
