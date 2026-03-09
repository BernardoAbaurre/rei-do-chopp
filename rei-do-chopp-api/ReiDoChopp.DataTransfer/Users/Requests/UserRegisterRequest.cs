using System.ComponentModel.DataAnnotations;

namespace ReiDoChopp.DataTransfer.Users.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CheckPassword { get; set; }

        public int[] RoleIds { get; set; }
    }
}
