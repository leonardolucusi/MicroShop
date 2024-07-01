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
            var userId = GetUserIdFromJWT.GetUserIdFromToken(Request.Cookies["jwt"]);

            var result = await _cartService.AddProductToCart(new AddProductToCartDTO { ProductId = productId, UserId = userId});

            if (result)
            {
                TempData["UserMessage"] = "Product added in cart.";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["UserMessage"] = "Product removed from cart.";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Index", "Products");
        }
    }
}
