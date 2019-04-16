using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IEventBase<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> PostByDate(string date);
    }
}
