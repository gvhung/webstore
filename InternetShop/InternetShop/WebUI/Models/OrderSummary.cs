using System;
using System.Collections.Generic;
using Entities;
using WebUI.Models.ForActivation;

namespace WebUI.Models
{
    public class OrderSummary
    {
        public OrderSummary()
        {
            Products = new Dictionary<Product, int>();
        }
        public int OrderId { get; set; }
        public UserProfile UserProfile { get; set; }
        public DateTime OrderDate { get; set; }
        public Dictionary<Product, int> Products { get; set; }
    }
}