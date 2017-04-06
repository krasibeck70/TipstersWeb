using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tipsters.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstUsername { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Image { get; set; }
        public string ImageBackground { get; set; }

        public virtual ICollection<ApplicationUser> OwnerFollowers { get; set; }

        public virtual ICollection<ApplicationUser> OwnerFollowing { get; set; }

        public virtual ICollection<Pronostic> OwnerPronostics { get; set; }

        public virtual ICollection<Comment> CommentsPronostics { get; set; }
    }
}
