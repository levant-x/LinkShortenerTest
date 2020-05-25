using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.BLL
{
    public abstract class ManagerBase
    {
        protected IRepository repository;

        public ManagerBase(IRepository repository)
        {
            this.repository = repository;
        }
    }
}
