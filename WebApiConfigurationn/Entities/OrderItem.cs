using WebApiConfigurationn.Entities.Common;

namespace WebApiConfigurationn.Entities
{
    public class OrderItem:BaseEntity
    {

        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
