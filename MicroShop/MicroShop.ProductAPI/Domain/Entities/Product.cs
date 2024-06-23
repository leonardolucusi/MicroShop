namespace MicroShop.ProductAPI.Domain.Entities
{
    public class Product
    {
        public Ulid Id { get; set; } = Ulid.NewUlid();
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
