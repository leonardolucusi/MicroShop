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
        private readonly ILogger<CartsController> _logger;
        public CartsController(ICartService cartService, IProductService productService, ILogger<CartsController> logger)
        {
            _cartService = cartService;
            _productService = productService;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProductDetailsDTO>>> GetAllCartItems()
        {
            try
            {
                var userId = TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"]);
                var cartItems = await _cartService.GetAllCartItemsInUserId(userId);
                if (cartItems.Count() == 0) return View(new List<CartProductDetailsDTO>());
                IEnumerable<string> productIds = cartItems.Select(ci => ci.ProductId);
                IEnumerable<ProductDTO> productsDto = await _productService.GetAllProductsByCartProductsIds(productIds);
                List<CartProductDetailsDTO> cartProductDetailsDTO = [];

                foreach (var item in productsDto)
                {
                    cartProductDetailsDTO.Add(new CartProductDetailsDTO
                    {
                        UserId = userId,
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(string productId)
        {
            try
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
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Unexpected error occurred"); 
                return StatusCode(500, new { message = "Unexpected error occurred" }); 
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantityProductInCartItem(UpdateCartItemDTO updateCartItemDTO)
        {
            try
            {
                var product = await _productService.GetProductById(updateCartItemDTO.ProductId);
                var cartItemProduct = await _cartService.GetOneProductfromCartItemByUserIdProductId(updateCartItemDTO.UserId, updateCartItemDTO.ProductId);
                if (updateCartItemDTO.AddOrRemove is true && cartItemProduct.Quantity >= product.Stock)
                {
                    TempData["ErrorMessage"] = "Estoque chegou ao limite.";
                    return RedirectToAction("GetAllCartItems", "Carts");
                }
                if (updateCartItemDTO.AddOrRemove == false && cartItemProduct.Quantity == 1)
                {
                    TempData["ErrorMessage"] = "Não pode ser zero";
                    return RedirectToAction("GetAllCartItems", "Carts");
                }
                await _cartService.UpdateOneProductInCartItemByUserIdProductId(updateCartItemDTO);
                return RedirectToAction("GetAllCartItems", "Carts");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAllUserCartItemsByUserId() 
        {
            try
            {
                await _cartService.DeleteAllUserCartItemsByUserId(TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"])); ;
                return RedirectToAction("GetAllCartItems", "Carts");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteOneCartItemInUser(string productId)
        {
            try
            {
                await _cartService.DeleteOneCartItemInUser(TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"]), productId);
                return RedirectToAction("GetAllCartItems", "Carts");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
    }
}
