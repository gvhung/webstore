using System;
using System.Collections.Generic;

namespace Entities
{
    public class Order:BaseEntity
    {
        public Order()
        {
            OrderProduct = new List<OrderProduct>();
        }
        public string ClientLogin { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public virtual List<OrderProduct> OrderProduct { get; set; }
    }
}