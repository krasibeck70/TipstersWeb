using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tipsters.Models.Models;

namespace Tipsters.Models.ViewModels.UsersViewMode
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }

        public DateTime CreatedUser { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<ApplicationUser> OwnerFollowers { get; set; }

        public virtual ICollection<ApplicationUser> OwnerFollowing { get; set; }

        public virtual ICollection<Pronostic> OwnerPronostics { get; set; }

        public virtual ICollection<Comment> CommentsPronostics { get; set; }

        public virtual ICollection<Pronostic> LikesPronostics { get; set; }

        public bool ContainsEmail(string email)
        {
            var emails = this.OwnerFollowers.Select(x => x.Email);
            if (emails.Contains(email))
            {
                return true;
            }
            return false;
        }
    }

    
}
