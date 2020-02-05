using System.Collections.Generic;

namespace TestWorkASP.Models
{
    public class Product
    {
        public Product()
        {
            OrderLines = new List<OrderLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }        
    }
}