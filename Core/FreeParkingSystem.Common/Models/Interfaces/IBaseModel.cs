using System;

// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Models
{
    public interface IBaseModel
    {
        string Id { get; set; }

        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset UpdatedAt { get; set; }

        bool IsDeleted { get; set; }

        IUser CreatedBy { get; set; }
    }
}
