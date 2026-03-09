using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace ReiDoChopp.Domain.Users.Entities
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; protected set; }
        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();


        public User(string firstName, string lastName, string email)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            CreationDate = DateTime.Now;
            UserName = email;
            SetEmail(email);
            SecurityStamp = Guid.NewGuid().ToString();
            SetActive(true);
        }

        public void SetFirstName(string firstName)
        {
            FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName);
        }

        public void SetLastName(string lastName)
        {
            LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastName);
        }
        public void SetActive(bool active)
        {
            Active = active;
        }
        
        public void SetEmail(string email)
        {
            Email = email;
            UserName = email;
        }

    }
}
