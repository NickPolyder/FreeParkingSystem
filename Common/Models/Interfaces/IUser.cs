// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Models
{
    public interface IUser : IBaseModel
    {

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string Phone { get; set; }

        bool Active { get; set; }

        string FullName();

        IRole Role { get; set; }

    }
}
