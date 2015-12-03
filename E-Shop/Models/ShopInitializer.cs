using EShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace E_Shop.Models
{
    public class ShopInitializer : DropCreateDatabaseAlways<ShopContext>
    {
        protected override void Seed(ShopContext db)
        {
            var adminRole = new IdentityRole { Name = "Administrators", Id = Guid.NewGuid().ToString() };
            db.Roles.Add(adminRole);
            var userRole = new IdentityRole { Name = "Users", Id = Guid.NewGuid().ToString() };
            db.Roles.Add(userRole);

            var hasher = new PasswordHasher();
            var admin = new ApplicationUser
            {
                UserName = "abacaba",
                PasswordHash = hasher.HashPassword("dabacaba"),
                Email = "test@test.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            db.Users.Add(admin);
            admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });

            var user = new ApplicationUser
            {
                UserName = "nyash",
                PasswordHash = hasher.HashPassword("myashkrim"),
                Email = "test@test.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            db.Users.Add(user);
            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
            db.SaveChanges();
            
            base.Seed(db);
        }
    }
}