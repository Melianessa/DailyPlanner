using System;

namespace DailyPlanner.DomainClasses.Interfaces
{
    interface IBase
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
        bool IsActive { get; set; }
    }

}
