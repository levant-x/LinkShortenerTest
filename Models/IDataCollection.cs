using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public interface IDataCollection<T> : IEnumerable<T>
    {
        T GetItem(Func<T, bool> predicate);
        void AddItem(T item);
        IEnumerable<T> GetItems(int count, int toSkip = 0);
        void SaveAdded(Func<T, bool> action);
    }
}
