using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class Link
    {
        public int ID { get; set; }
        public string ShortURL { get; set; }
        public string URL { get; set; }
    }
}
