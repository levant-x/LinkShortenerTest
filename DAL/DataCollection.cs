using LinkShortener.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LinkShortener.DAL
{
    public class DataCollection<T> : IDataCollection<T>
    {
        protected IQueryable<T> dataProvider;
        protected List<T> itemsToAdd = new List<T>();


        public DataCollection(IQueryable<T> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public void AddItem(T item)
        {
            itemsToAdd.Add(item);
        }

        public T GetItem(Func<T, bool> predicate)
        {
            return dataProvider.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetItems(int count, int toSkip = 0)
        {
            return dataProvider.Skip(toSkip)
                .Take(count);
        }

        public void SaveAdded(Func<T, bool> action)
        {
            var itemsSaved = new Stack<T>();
            foreach (var item in itemsToAdd)
                if (action(item)) itemsSaved.Push(item);
            while (itemsSaved.Count > 0) itemsToAdd.Remove(itemsSaved.Pop());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dataProvider.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dataProvider.GetEnumerator();
        }
    }
}
