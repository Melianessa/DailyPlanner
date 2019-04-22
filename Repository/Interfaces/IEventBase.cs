using System;
using System.Collections.Generic;
using System.Text;
using Repository.Models;

namespace Repository.Interfaces
{
    public interface IEventBase<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetByDate(string date);
    }
}
