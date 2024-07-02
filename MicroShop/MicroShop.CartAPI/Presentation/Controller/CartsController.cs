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
        private readonly ILogger<CartsController> _logger;
        public CartsController(ICartService cartService, IMapper mapper, ILogger<CartsController> logger)
        {
            _cartService = cartService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveProductToCart([FromBody] AddProductToCartDTO addProductToCartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var productAddedOrRemoved = await _cartService.AddOrRemoveCartItemAsync(addProductToCartDTO.UserId, addProductToCartDTO.ProductId);
            return Ok(productAddedOrRemoved);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInCartItemProductQuantity([FromBody] UpdateProductQuantityInCartItemDTO updateDto)
        {
            if (await _cartService.UpdateQuantityInCartItemProduct(updateDto)) return Ok();
            return BadRequest();
        }

        [HttpGet("{userId}/cartItems")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetAllCartItemsInUser(int userId)
        {
            return Ok(await _cartService.GetAllCartItemsByUserId(userId));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllCartItemsByUserId(int userId)
        {
            try
            {
                bool result = await _cartService.DeleteAllCartItemsByUserId(userId);

                if (result)
                {
                    return Ok(new { Message = "Todos os itens do carrinho foram deletados com sucesso." });
                }
                else
                {
                    return StatusCode(500, new { Message = "Erro ao deletar os itens do carrinho." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar itens do carrinho para o usuário {userId}: {ex.Message}");
                return StatusCode(500, new { Message = "Erro interno do servidor ao processar a solicitação." });
            }
        }
    }
}
