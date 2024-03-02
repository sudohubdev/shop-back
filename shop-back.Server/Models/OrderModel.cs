namespace shop_back.Server.Models
{
    public class OrderModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string LegalName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostOffice { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
    }
}

