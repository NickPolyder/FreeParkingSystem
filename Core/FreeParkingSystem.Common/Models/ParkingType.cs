using System;

namespace FreeParkingSystem.Common.Models
{
    public class ParkingType : BaseModel, IParkingType
    {
        public ParkingType()
        { }

        public ParkingType(string text, IUser createdBy = null) : this(Guid.NewGuid().ToString(), text, createdBy)
        { }

        public ParkingType(string id, string text, IUser createdBy = null)
        {
            Id = !string.IsNullOrEmpty(id) ? id : Guid.NewGuid().ToString();

            CreatedBy = createdBy ?? User.SysAdmin;

            Text = text ?? throw new ArgumentNullException(nameof(text));

            CreatedAt = UpdatedAt = DateTime.Now;

            IsDeleted = false;
        }

        public string Text { get; set; }

        private static Lazy<IParkingType> _free = new Lazy<IParkingType>(() => new ParkingType("Free"), true);

        public static IParkingType Free
        {
            get
            {
                return _free.Value;
            }
        }

        private static Lazy<IParkingType> _paid = new Lazy<IParkingType>(() => new ParkingType("Paid"), true);

        public static IParkingType Paid
        {
            get
            {
                return _paid.Value;
            }
        }
    }
}
