namespace MicroShop.Web.Domain.DTOs.CartDTOs
{
    public class UpdateCartItemDTO
    {
        public int UserId { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public bool AddOrRemove { get; set; }
    }
}
