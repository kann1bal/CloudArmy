using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;


namespace MAP.Presentation.Models
{
    public class UserModel : IdentityUser<int, CustomLogin, CustomUserRole, CustomUserClaim>
    {


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel, int> manager)
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
