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
    public class ShopInitializer : DropCreateDatabaseIfModelChanges<ShopContext>
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
                SecurityStamp = Guid.NewGuid().ToString(),
                UserOrders = new List<Order>()
            };
            db.Users.Add(admin);
            admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });

            var user = new ApplicationUser
            {
                UserName = "nyash",
                PasswordHash = hasher.HashPassword("myashkrim"),
                Email = "test@test.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserOrders = new List<Order>()
            };
            db.Users.Add(user);
            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
            db.SaveChanges();


            var item1 = new Item { Name = "RedBull", Price = 96.0F, Description = "Energy drink", Category = Category.F, Image = "https://static.labdoor.com/images/product/2000/red-bull-68-a.jpg", ForSale = true };
            var item2 = new Item { Name = "Donut", Price = 55.0F, Description = "Tasty!", Category = Category.A, Image = "http://www.withsprinklesontop.net/wp-content/uploads/2012/01/DSC_0406x900.jpg" };
            var item3 = new Item { Name = "Beer", Price = 196.0F, Description = "Not so tasty", Category = Category.F, Image = "http://i.huffpost.com/gen/1294334/images/o-JUST-ADD-WATER-BEER-facebook.jpg" };
            var item4 = new Item { Name = "Chair", Price = 996.0F, Description = "Shouldn\'t eat that", Category = Category.B, Image = "http://www.hercampus.com/sites/default/files/2015/02/05/walnut_wood_ladder_back_restaurant_chair_-_solid_wood_seat__1.jpg", ForSale = true };

            db.Items.Add(item1);
            db.Items.Add(item2);
            db.Items.Add(item3);
            db.Items.Add(item4);
            db.SaveChanges();
            base.Seed(db);
        }
    }
}