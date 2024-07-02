namespace MicroShop.Web.Domain.DTOs.CartDTOs
{
    public class CartProductDetailsDTO
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public int ProductQuantity { get; set; }
    }
}
