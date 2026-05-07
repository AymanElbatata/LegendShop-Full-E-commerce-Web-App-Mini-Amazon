using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class UserViewDTO
    {
        public string? Id { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public bool isActivated { get; set; } = false;
        public bool IsSwitchedOff { get; set; } = false;

        public List<string> Roles { get; set; } = new List<string>();

        public string UserName { get; set; } = null!;

        //public List<AppRole> Roles = new List<AppRole>();
    }

    public class UserInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<string> SelectedRoles { get; set; } = new List<string>();

        public string UserName { get; set; }

        public bool isActivated { get; set; } = false;
        public bool isDeleted { get; set; } = false;
    }

    public class UserInputModelUpdate
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<string> SelectedRoles { get; set; } = new List<string>();

        public string UserName { get; set; }

        public bool isDeleted { get; set; } = false;
        public bool isActivated { get; set; } = false;

    }

    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
