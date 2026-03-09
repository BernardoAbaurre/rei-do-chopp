using ReiDoChopp.Domain.Roles.Repositories.Models;

namespace ReiDoChopp.Domain.Users.Repositories.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }
        public bool Active { get; set; }
    }
}
