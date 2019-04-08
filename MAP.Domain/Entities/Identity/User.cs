using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;


namespace MAP.Domain.Entities
{
    public class User : IdentityUser<int, CustomLogin, CustomUserRole, CustomUserClaim>
    {
        public int status { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Logo { get; set; }
        public virtual ICollection<Meeting> meetings { get; set; }
        public virtual ICollection<Tasks> tasks { get; set; }
        public virtual ICollection<Request> requests { get; set; }


        public User()
        {
            status = 0;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }



    }


    public class CustomLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }
    public class CustomUserRole : IdentityUserRole<int>
    {
        public int Id { get; set; }
    }
    public class CustomUserClaim : IdentityUserClaim<int>
    {

    }
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

}
