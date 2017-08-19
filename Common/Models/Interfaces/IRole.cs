namespace FreeParkingSystem.Common.Models
{
    public interface IRole : IBaseModel
    {
        string Description { get; set; }

        AccessLevel AccessLevel { get; set; }
    }
}
