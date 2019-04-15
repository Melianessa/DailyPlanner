using System;

namespace Repository.Interfaces
{
    interface IBase
    {
        Guid Id { get; set; }
        long CreationDate { get; set; }
        bool IsActive { get; set; }
    }

}
