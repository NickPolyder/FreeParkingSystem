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

        public static IRole Anonymous()
        {
            return new Role();
        }

        public static IRole Administrator()
        {
            return new Role(AccessLevel.Administrator);
        }
    }
}
