using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public interface IRepository
    {
        IDataCollection<Link> Links { get; }
        DataQueryResult SaveChanges();
    }
}
