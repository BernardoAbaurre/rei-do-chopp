using ReiDoChopp.DataTransfer.Roles;
using ReiDoChopp.DataTransfer.Roles.Responses;

namespace ReiDoChopp.DataTransfer.Users.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public IList<RoleResponse> Roles { get; set; }
        public bool Active { get; set; }
    }
}
