namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public string pictureUrl { get; set; }
        public string Brand { get; set; }
        public string type { get; set; }
    }
}
