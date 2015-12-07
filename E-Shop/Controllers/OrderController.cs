using EShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            if (user.UserOrders == null)
                user.UserOrders = new List<Order>();
            CurrentCart.Client = userName;
            user.UserOrders.Add(CurrentCart);
            db.SaveChanges();
            Session["CartId"] = null;
            return View();
        }

        public ActionResult Admin()
        {
            List<Order> tmp = db.Orders.ToList();
            tmp.RemoveAll(item => item.Client == null);
            return View(tmp);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Remove(db.Orders.Single(r => r.OrderID == order.OrderID));
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(order);
        }
    }
}
