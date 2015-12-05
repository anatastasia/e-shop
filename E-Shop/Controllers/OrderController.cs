using EShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class OrderController : Controller
    {
        private static ShopContext db = new ShopContext();
        private Order CurrentCart;

      public OrderController()
        {
            if (System.Web.HttpContext.Current.Session["CartId"] == null)
            {
                CurrentCart = new Order();
                CurrentCart.OrderItems = new List<Item>();
                db.Orders.Add(CurrentCart);
                System.Web.HttpContext.Current.Session["CartId"] = CurrentCart.OrderID;
            }
            else
                CurrentCart = db.Orders.Find(System.Web.HttpContext.Current.Session["CartId"]);
        }

        public ActionResult AddToCart(int id)
        {
            CurrentCart.OrderItems.Add(db.Items.Find(id));
            return RedirectToAction("Cart");
        }

        public ActionResult RemoveFromCart(int id)
        {
            CurrentCart.OrderItems.Remove(db.Items.Find(id));
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            return View(CurrentCart.OrderItems);
        }

        public ActionResult Checkout()
        {
            CurrentCart.OrderDate = DateTime.Now;
            CurrentCart.Status = Status.inProcess;
            db.Orders.Add(CurrentCart);
            string userName = User.Identity.Name;
            var user = db.Users.Where(tmp => tmp.UserName == userName).ToList().First();
            user.Orders.Add(CurrentCart);
            db.SaveChanges();
            return View();
        }
    }
}
