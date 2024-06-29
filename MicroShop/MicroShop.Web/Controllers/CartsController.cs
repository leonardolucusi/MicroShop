using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.CartDTOs;
using MicroShop.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Web.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(string productId)
        {
            await _cartService.AddProductToCart(new AddProductToCartDTO { ProductId = productId, UserId = GetUserIdFromJWT.GetUserIdFromToken(Request.Cookies["jwt"])});
            return RedirectToAction("Index", "Product");
        }
    }
}
