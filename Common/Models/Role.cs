namespace FreeParkingSystem.Common.Models
{
    public class Role : BaseModel, IRole
    {
        public string Description { get; set; }

        public AccessLevel AccessLevel { get; set; }
    }
}
