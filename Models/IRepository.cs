using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public interface IRepository
    {
        IDataCollection<LinkModel> Links { get; }
        IDataCollection<UserBaseModel> Users { get; }
        QueryResult SaveChanges();
    }
}
