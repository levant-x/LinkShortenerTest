using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public interface IDataCollection<T>
    {
        IQueryable<T> GetAll();
        T GetByKey(string key, string value);
    }
}
