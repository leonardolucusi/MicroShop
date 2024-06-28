namespace MicroShop.CartAPI.Domain.DTOs
{
    public class AddProductToCartDTO
    {
        public int userId { get; set; }
        public string? productId { get; set; }
    }
}
