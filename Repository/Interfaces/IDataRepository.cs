using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IDataRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll ();
        TEntity Get(Guid id);
        void Add(TEntity b);
        void Update(Guid id, TEntity b);
        void Delete(TEntity b);
    }
}
