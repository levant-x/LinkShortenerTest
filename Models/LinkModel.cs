using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class LinkModel
    {
        public int ID { get; set; }
        public string ShortURL { get; set; }
        public string URL { get; set; }
        public DateTime Created { get; set; }
    }
}
