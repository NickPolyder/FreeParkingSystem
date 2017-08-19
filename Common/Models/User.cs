namespace FreeParkingSystem.Common.Models
{
    public class User : BaseModel, IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public bool Active { get; set; }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

        public IRole Role { get; set; }
    }
}
