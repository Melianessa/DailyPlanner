using System.Collections.Generic;

namespace DailyPlanner.DomainClasses.Interfaces
{
    public interface IEventBase<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetByDate(string date);
    }
}
