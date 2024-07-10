using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.CartDTOs;
using MicroShop.Web.Domain.DTOs.ProductDTOs;
using MicroShop.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _productApiClient;
        private readonly HttpClient _cartApiClient;
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IHttpClientFactory httpClientFactory, IProductService productService, IHttpClientFactory cartApiClient, ILogger<ProductsController> logger)
        {
            _productApiClient = httpClientFactory.CreateClient("ProductAPI");
            _cartApiClient = httpClientFactory.CreateClient("CartAPI");
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (TokenManipulator.IsTokenExpired(Request.Cookies["jwt"]))
                {
                    Response.Cookies.Delete("jwt");
                    return RedirectToAction("Index", "Products");
                }
                var userId = TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"]);
                HttpResponseMessage response = await _productApiClient.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    List<ProductDTO> products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();

                    HttpResponseMessage cartResponse = await _cartApiClient.GetAsync($"api/v1/carts/{userId}/cartItems");

                    if (cartResponse.IsSuccessStatusCode)
                    {
                        List<CartItemDTO> cartItems = await cartResponse.Content.ReadFromJsonAsync<List<CartItemDTO>>();

                        foreach (var product in products)
                        {
                            product.IsInCart = cartItems.Any(ci => ci.ProductId == product.Id);
                        }
                    }
                    return View(products);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult ProductCreatePage()
        {
            return View();
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDTO productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.CreateProduct(productDto);
                    return RedirectToAction("Index");
                }
                return View(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ProductUpdatePage(string id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDTO productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _productService.UpdateProduct(productDto);
                    if (response != null) return RedirectToAction(
                        nameof(Index));
                }
                return View(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ProductDeletePage(string id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult> ProductDelete(string id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);
                if (response) return RedirectToAction(nameof(Index));
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(500, new { message = "Unexpected error occurred" });
            }
        }
    }
}
