using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public enum Status
    {
        inProcess, shipped, delivered, cancelled
    }

    public class Order
    {
        public int OrderID { get; set; }
        public Status? Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Item> OrderItems { get; set; }
        public string Client { get; set; }
    }
}