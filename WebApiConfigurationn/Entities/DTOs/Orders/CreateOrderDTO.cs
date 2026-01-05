namespace WebApiConfigurationn.Entities.DTOs.Orders
{
    public class CreateOrderDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
