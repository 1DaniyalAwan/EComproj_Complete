using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EComproj.Models;

namespace EComproj.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using EComproj.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EComproj.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EComproj.Models.ApplicationDbContext context)
        {
            // Role manager and user manager using the same context
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // Relax password policy so we can use "12345" (demo only)
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // Create roles if they don't exist
            string[] roles = { "Admin", "Seller", "Customer" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            // Seed admin user
            var adminEmail = "daniyalawan12317@gmail.com";
            var adminPassword = "12345";

            var adminUser = userManager.FindByEmail(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = userManager.Create(adminUser, adminPassword);
                if (!createResult.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " + string.Join("; ", createResult.Errors));
                }
            }

            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            // Seed default categories
            string[] defaultCategories = {
                "Electronics", "Fashion", "Home & Kitchen", "Books",
                "Sports & Outdoors", "Beauty & Personal Care", "Toys & Games"
            };

            foreach (var catName in defaultCategories)
            {
                if (!context.Categories.Any(c => c.Name == catName))
                {
                    context.Categories.Add(new Category { Name = catName });
                }
            }

            context.SaveChanges();
        }
    }
}
