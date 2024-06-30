namespace MicroShop.CartAPI.Domain.DTOs
{
    public class AddProductToCartDTO
    {
        public int UserId { get; set; }
        public string? ProductId { get; set; }
    }
}
