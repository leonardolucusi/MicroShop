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
        [HttpPut]
        public async Task<IActionResult> UpdateQuantityProductInCartItem(UpdateCartItemDTO updateCartItemDTO)
        {
            var product = await _productService.GetProductById(updateCartItemDTO.ProductId);
            var stock = product.Stock;
            var cartItemProduct = await _cartService.GetOneProductfromCartItemByUserIdProductId(updateCartItemDTO.UserId, updateCartItemDTO.ProductId);
            if (cartItemProduct.Quantity >= stock) return BadRequest();

            return Ok(await _cartService.UpdateOneProductInCartItemByUserIdProductId(updateCartItemDTO));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProductDetailsDTO>>> GetAllCartItems()
        {
            var cartItems = await _cartService.GetAllCartItemsInUserId(
                TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"]));
            if (cartItems.Count() == 0) return View(new List<CartProductDetailsDTO>());
            IEnumerable<string> productIds = cartItems.Select(ci => ci.ProductId);
            IEnumerable<ProductDTO> productsDto = await _productService.GetAllProductsByCartProductsIds(productIds);
            List<CartProductDetailsDTO> cartProductDetailsDTO = [];

            foreach (var item in productsDto)
            {
                cartProductDetailsDTO.Add(new CartProductDetailsDTO
                {
                    ProductId = item.Id,
                    ProductName = item.Name,
                    ProductPrice = item.Price
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
