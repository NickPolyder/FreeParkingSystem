using FreeParkingSystem.Common.Helpers;
using System;

namespace FreeParkingSystem.Common.Models
{
    public class Role : BaseModel, IRole
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public Role() : this(AccessLevel.Anonymous)
        { }

        public Role(AccessLevel accessLevel) : this(accessLevel, accessLevel.ToString(), accessLevel.ToString())
        { }

        public Role(AccessLevel accessLevel, string name, string description)
        {
            Id = System.Guid.NewGuid().ToString();
            AccessLevel = accessLevel;
            Name = name;
            Description = description;
            CreatedAt = UpdatedAt = System.DateTimeOffset.Now;
        }

        public override bool Equals(object obj) => RoleEqualityComparer.Current.Equals(this, obj as IRole);

        public override int GetHashCode() => RoleEqualityComparer.Current.GetHashCode(this);


        private static Lazy<IRole> _anonymous = new Lazy<IRole>(() => new Role(), true);

        public static IRole Anonymous()
        {
            return _anonymous.Value;
        }

        private volatile static Lazy<IRole> _administrator = new Lazy<IRole>(() => new Role(AccessLevel.Administrator), true);

        public static IRole Administrator()
        {
            return _administrator.Value;
        }

        private volatile static Lazy<IRole> _member = new Lazy<IRole>(() => new Role(AccessLevel.Member), true);

        public static IRole Member()
        {
            return _member.Value;
        }
    }
}
