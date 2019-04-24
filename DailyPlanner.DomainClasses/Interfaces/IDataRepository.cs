using System;
using System.Collections.Generic;

namespace DailyPlanner.DomainClasses.Interfaces
{
    public interface IDataRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll ();
        TEntity Get(Guid id);
        Guid Add(TEntity b);
        TEntity Update(Guid id, TEntity b);
        int Delete(TEntity b);
    }
}
