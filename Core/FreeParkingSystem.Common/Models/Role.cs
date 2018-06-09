using FreeParkingSystem.Common.Helpers;

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

        private static object _anonymousLock = new object();
        private volatile static IRole _anonymous;

        public static IRole Anonymous()
        {
            lock (_anonymousLock)
            {
                if (_anonymous == null)
                {
                    _anonymous = new Role();
                }
            }
            return _anonymous;
        }

        private static object _administratorLock = new object();
        private volatile static IRole _administrator;

        public static IRole Administrator()
        {
            lock (_administratorLock)
            {
                if (_administrator == null)
                {
                    _administrator = new Role(AccessLevel.Administrator);
                }
            }
            return _administrator;
        }

        private static object _memberLock = new object();
        private volatile static IRole _member;
        
        public static IRole Member()
        {
            lock (_memberLock)
            {
                if (_member == null)
                {
                    _member = new Role(AccessLevel.Member);
                }
            }

            return _member;
        }
    }
}
