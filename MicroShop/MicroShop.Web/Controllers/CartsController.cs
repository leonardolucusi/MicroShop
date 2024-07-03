using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.CartDTOs;
using MicroShop.Web.Domain.DTOs.ProductDTOs;
using MicroShop.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Web.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartsController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(string productId)
        {
            var userId = TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"]);
            var result = await _cartService.AddProductToCart(new AddProductToCartDTO { ProductId = productId, UserId = userId });
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProductDetailsDTO>>> GetAllCartItems()
        {
            var userId = TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"]);
            var cartItems = await _cartService.GetAllCartItemsInUserId(userId);
            if (cartItems.Count() == 0) return View(new List<CartProductDetailsDTO>());
            foreach (var item in cartItems)
            {
                await Console.Out.WriteLineAsync(item.ProductId);
            };
            IEnumerable<string> productIds = cartItems.Select(ci => ci.ProductId);

            IEnumerable<ProductDTO> productsDto = await _productService.GetAllProductsByCartProductsIds(productIds);
            List<CartProductDetailsDTO> cartProductDetailsDTO = [];

            foreach (var item in productsDto)
            {
                cartProductDetailsDTO.Add(new CartProductDetailsDTO
                {
                    ProductId = item.Id,
                    ProductName = item.Name,
                    ProductPrice = (decimal)item.Price
                });
            }
            foreach (var cartItem in cartItems)
            {
                var cartProductDetail = cartProductDetailsDTO.FirstOrDefault(dto => dto.ProductId == cartItem.ProductId);

                if (cartProductDetail != null)
                {
                    cartProductDetail.ProductQuantity = cartItem.Quantity;
                }
            }
            return View(cartProductDetailsDTO);
        }
    }
}
