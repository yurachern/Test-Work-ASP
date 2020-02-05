using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWorkASP.Models
{
    public class Record
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }       
        public string Name { get; set; }
        public string Adress { get; set; }
        public decimal Sum { get; set; }
        public int CustomerId { get; set; }
    }
}