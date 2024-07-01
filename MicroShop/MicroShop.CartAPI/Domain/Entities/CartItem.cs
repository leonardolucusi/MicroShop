namespace MicroShop.CartAPI.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}