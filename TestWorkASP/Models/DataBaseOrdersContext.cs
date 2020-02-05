namespace TestWorkASP.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataBaseOrdersContext : DbContext
    {       
        public DataBaseOrdersContext(): base("name=DataBaseOrdersContext")
        {          
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}