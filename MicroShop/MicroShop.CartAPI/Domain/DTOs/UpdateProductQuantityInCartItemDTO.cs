namespace MicroShop.CartAPI.Domain.DTOs
{
    public class UpdateProductQuantityInCartItemDTO
    {
        public int UserId { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
