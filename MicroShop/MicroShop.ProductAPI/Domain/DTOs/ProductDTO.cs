﻿namespace MicroShop.ProductAPI.Domain.DTOs
{
    public class ProductDTO
    {
        public Ulid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
