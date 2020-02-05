using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWorkASP.Models
{
    public class Order
    {
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
    }
}