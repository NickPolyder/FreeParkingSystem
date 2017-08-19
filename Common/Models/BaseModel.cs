using System;

namespace FreeParkingSystem.Common.Models
{
    public class BaseModel : IBaseModel
    {
        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public IUser CreatedBy { get; set; }

    }
}
