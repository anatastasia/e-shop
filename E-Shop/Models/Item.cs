using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public enum Category
    {
        A, B, C, D, F
    }

    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public Category? Category { get; set; }
        public string Description { get; set; }
        public bool ForSale { get; set; }
    }
}