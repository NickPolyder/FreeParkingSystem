using System.ComponentModel.DataAnnotations;

// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Models
{
    public interface IUser : IBaseModel
    {
        [Required]
        string FirstName { get; set; }

        [Required]
        string LastName { get; set; }

        [Required]
        [EmailAddress]
        string Email { get; set; }

        string Password { get; set; }

        [Phone]
        string Phone { get; set; }

        bool Active { get; set; }

        string FullName();

        [Required]
        IRole Role { get; set; }

    }
}
