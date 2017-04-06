using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Tipsters.Data.Interfaces;
using Tipsters.Models;
using Tipsters.Models.Models;

namespace Tipsters.Data
{
    public class TipstersContext : IdentityDbContext<ApplicationUser>,ITipstersContext
    {

        public TipstersContext()
            : base("TipstersContext", throwIfV1Schema: false)
        {
        }

        public static TipstersContext Create()
        {
            return new TipstersContext();
        }
        
        public virtual IDbSet<Team> Teams { get; set; }
        public virtual IDbSet<County> Countries { get; set; }
        public virtual IDbSet<League> Leagues { get; set; }
        public virtual IDbSet<Pronostic> Pronostics { get; set; }
        public virtual IDbSet<Tip> Tips { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public DbContext DbContext => this;

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.OwnerFollowers).WithMany(x => x.OwnerFollowing)
                .Map(x => x.ToTable("Followers")
                    .MapLeftKey("UserId")
                    .MapRightKey("FollowerId"));
            modelBuilder.Entity<Comment>()
                .HasRequired(x => x.Pronostic)
                .WithMany(x => x.OwnerComments)
                .HasForeignKey(x => x.PronosticId);
            modelBuilder.Entity<Comment>()
                .HasRequired(x => x.User)
                .WithMany(x => x.CommentsPronostics)
                .HasForeignKey(x => x.UserId);
        }
    }
}
