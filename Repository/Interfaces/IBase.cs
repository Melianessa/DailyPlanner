using System;

namespace Repository.Interfaces
{
    interface IBase
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
        bool IsActive { get; set; }
    }

}
