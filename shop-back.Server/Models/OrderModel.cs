namespace shop_back.Server.Models
{
    public class OrderModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public required string LegalName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string City { get; set; }
        public required string PostOffice { get; set; }
        public required string DeliveryMethod { get; set; }
        public required string PaymentMethod { get; set; }
    }
}

