using AutoMapper;
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
        private readonly IMapper _mapper;
        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveProductToCart([FromBody] AddProductToCartDTO addProductToCartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _cartService.AddOrRemoveProductToCartAsync(addProductToCartDTO.userId, addProductToCartDTO.productId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInCartItemProductQuantity([FromBody] UpdateProductQuantityInCartItemDTO updateDto)
        {
            if (await _cartService.UpdateQuantityInCartItemProduct(updateDto)) return Ok();
            return BadRequest();
        }

        [HttpGet("{cartId}/cartItems")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetAllCartItemsInCart(int cartId)
        {
            var cartItems = await _cartService.GetAllCartItems(cartId);
            return Ok(cartItems);
        }
    }
}
