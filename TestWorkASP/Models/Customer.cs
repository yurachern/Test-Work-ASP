using System.Collections.Generic;

namespace TestWorkASP.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Category { get; set; }

        public virtual List<Order> Orders { get; set; }      
    }
}