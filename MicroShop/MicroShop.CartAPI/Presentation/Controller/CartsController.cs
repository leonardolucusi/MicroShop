using AutoMapper;
using MicroShop.CartAPI.Application.Interfaces;
using MicroShop.CartAPI.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace MicroShop.CartAPI.Presentation.Controller
{

    [ApiController]
    [Route("api/v1/carts")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public CartsController(ICartService cartService, IMapper mapper)
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
            return Ok(await _cartService.GetAllCartItems(cartId));
        }
    }
}
