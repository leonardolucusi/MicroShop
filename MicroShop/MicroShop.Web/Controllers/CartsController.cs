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

            var added = await _cartService.AddProductToCart(new AddProductToCartDTO { ProductId = productId, UserId = userId });

            if (added)
            {
                TempData[$"Product_{productId}"] = "added";
                TempData["UserMessage"] = "Product added to cart successfully!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData[$"Product_{productId}"] = "removed";
                TempData["UserMessage"] = "Product removed from cart.";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index", "Products");
        }
    }
}
