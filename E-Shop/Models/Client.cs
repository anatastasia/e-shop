using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public class ApplicationUser : IdentityUser
    {      
        public List<Order> UserOrders { get; set; }
    }
}