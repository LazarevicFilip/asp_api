namespace Domain.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string BookPublisher { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}