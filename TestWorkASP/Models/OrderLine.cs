namespace TestWorkASP.Models
{
    public class OrderLine
    {      
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Sum { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}