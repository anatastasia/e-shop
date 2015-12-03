using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EShop.Models;

namespace EShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //Database.SetInitializer<ShopContext>(new ShopInitializer());
            ShopContext db = new ShopContext();
            db.Database.Initialize(true);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
