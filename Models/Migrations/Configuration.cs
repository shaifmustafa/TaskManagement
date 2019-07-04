using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.DatabaseContext;
using Models.Entity_Models;

namespace Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.DatabaseContext.TaskMDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.DatabaseContext.TaskMDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            addRoles(context);
            addUsers(context);
        }

        private void addUsers(TaskMDbContext dbContext)
        {
            string email = "admin@gmail.com";
            ApplicationUser user = dbContext.Users.FirstOrDefault(x => x.Email == email);

            if (user==null)
            {
                IdentityRole identityRole = dbContext.Roles.First(x => x.Name == "Admin");
                var userId = Guid.NewGuid().ToString();
                PasswordHasher hasher = new PasswordHasher();
                string pass = hasher.HashPassword("Pass123");

                IdentityUserRole userRole = new IdentityUserRole()
                {
                    UserId =userId, RoleId = identityRole.Id
                };
                user = new ApplicationUser()
                {
                    Email = email,
                    UserName = email,
                    Id = userId,
                    Roles = { userRole},
                    PasswordHash = pass
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        private void addRoles(TaskMDbContext dbContext)
        {
            List<string> roles = new List<string>()
            {
                "Admin", "User"
            };
            foreach (string r in roles)
            {
                IdentityRole identityRole = dbContext.Roles.FirstOrDefault(x => x.Name == r);
                if (identityRole == null)
                {
                   identityRole = new IdentityRole(r);
                   dbContext.Roles.Add(identityRole);
                }
            }

            dbContext.SaveChanges();
        }
    }
}
