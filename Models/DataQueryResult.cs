using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class DataQueryResult
    {
        public bool IsSuccessful
        {
            get => Errors.Length == 0;
        }

        public string[] Errors { get; set; }
    }
}
