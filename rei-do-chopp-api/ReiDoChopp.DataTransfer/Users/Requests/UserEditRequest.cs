using System.ComponentModel.DataAnnotations;

namespace ReiDoChopp.DataTransfer.Users.Requests
{
    public class UserEditRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int[] RoleIds { get; set; }

    }
}
