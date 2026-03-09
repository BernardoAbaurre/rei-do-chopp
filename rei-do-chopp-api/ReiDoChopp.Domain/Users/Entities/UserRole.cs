using Microsoft.AspNetCore.Identity;
using ReiDoChopp.Domain.Roles.Entities;

namespace ReiDoChopp.Domain.Users.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }
    }
}
