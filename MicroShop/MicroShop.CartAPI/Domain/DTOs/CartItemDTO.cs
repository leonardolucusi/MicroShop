﻿namespace MicroShop.CartAPI.Domain.DTOs
{
    public class CartItemDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
