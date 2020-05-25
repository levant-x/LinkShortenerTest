using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class QueryResult
    {
        public bool IsSuccessful
        {
            get => Errors == null;
        }

        public string[] Errors { get; set; }
        public object Data { get; set; }
    }
}
