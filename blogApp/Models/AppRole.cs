using Microsoft.AspNetCore.Identity;

namespace blogApp.Models
{
    public class AppRole : IdentityRole<long>
    {
        public AppRole() : base()
        {

        }

        public AppRole(string roleName)
        {
            Name = roleName;
        }
    }
}
