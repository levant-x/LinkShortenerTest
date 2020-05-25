using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public interface IRepository
    {
        IDataCollection<LinkModel> Links { get; }
        IDataCollection<UserModel> Users { get; }
        DataQueryResult SaveChanges();
    }
}
