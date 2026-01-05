namespace WebApiConfigurationn.Entities.DTOs.OrderItems
{
    public class UpdateOrderItemDTO
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
