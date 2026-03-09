using System.ComponentModel.DataAnnotations;

namespace ReiDoChopp.DataTransfer.Users.Requests
{
    public class UserResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CheckPassword { get; set; }
    }
}
