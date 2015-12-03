using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using E_Shop.Models;

namespace EShop.Models
{
    public class ShopContext : IdentityDbContext<ApplicationUser>
    {
        public ShopContext() : base("ShopContext") {
            Database.SetInitializer( new ShopInitializer() );
        }

        public static ShopContext Create()
        {
            return new ShopContext();
        }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}