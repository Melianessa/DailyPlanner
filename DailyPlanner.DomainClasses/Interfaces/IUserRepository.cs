using System;
using System.Collections.Generic;
using System.Text;
using DailyPlanner.DomainClasses.Models;

namespace DailyPlanner.DomainClasses.Interfaces
{
    public interface IUserRepository : IDataRepository<User>
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUser(Guid id);
    }
}
