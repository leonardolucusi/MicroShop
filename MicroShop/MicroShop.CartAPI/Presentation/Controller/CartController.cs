using MicroShop.CartAPI.Application.Interfaces;
using MicroShop.CartAPI.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace MicroShop.CartAPI.Presentation.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveProductToCart([FromBody] AddProductToCartDTO addProductToCartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _cartService.AddOrRemoveProductToCartAsync(addProductToCartDTO.userId, addProductToCartDTO.productId);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInCartItemProductQuantity([FromBody] int cartId, string productId, int quantity)
        {
            UpdateProductQuantityInCartItemDTO updateDto = new UpdateProductQuantityInCartItemDTO { CartId = cartId, ProductId = productId, Quantity = quantity };
            await _cartService.UpdateQuantityInCartItemProduct(updateDto);
            return Ok();
        }

        [HttpPut("UpdateV2")]
        public async Task<IActionResult> UpdateInCartItemProductQuantity([FromBody] UpdateProductQuantityInCartItemDTO updateDto)
        {
            if (await _cartService.UpdateQuantityInCartItemProduct(updateDto)) return Ok();
            return BadRequest();
        }
    }
}