using Microsoft.AspNet.Identity.EntityFramework;
using Tipsters.Models.Models;

namespace Tipsters.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tipsters.Data.TipstersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Tipsters.Data.TipstersContext";
        }

        protected override void Seed(Tipsters.Data.TipstersContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            if (!roleManager.Roles.Any())
            {
                var roleCreated = roleManager.Create(new IdentityRole("Admin"));
                if (roleCreated.Succeeded)
                {
                    var user = userManager.Users.First(x => x.Email == "krasibeck70@gmail.com");
                    var user2 = userManager.Users.First(x => x.Email == "stanimiraalek@gmail.com");
                    userManager.AddToRole(user.Id,"Admin");
                    userManager.AddToRole(user2.Id, "Admin");
                }
            }
           

            // Check to see if Role Exists, if not create it
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
