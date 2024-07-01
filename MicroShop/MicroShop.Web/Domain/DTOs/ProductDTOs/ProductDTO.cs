namespace MicroShop.Web.Domain.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public bool IsInCart { get; set; }
    }
}
