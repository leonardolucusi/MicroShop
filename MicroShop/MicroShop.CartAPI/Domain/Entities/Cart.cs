namespace MicroShop.CartAPI.Domain.Entities
{
    public class Cart
    {
        public int Id { get; private set; }
        public int UserId { get; set; }
        public virtual List<CartItem>? CartItems { get; set; } = new List<CartItem>();
    }
}
