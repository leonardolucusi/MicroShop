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
        public ProductsController(IHttpClientFactory httpClientFactory, IProductService productService , IHttpClientFactory cartApiClient)
        {
            _productApiClient = httpClientFactory.CreateClient("ProductAPI");
            _cartApiClient = httpClientFactory.CreateClient("CartAPI");
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserIdFromJWT.GetUserIdFromToken(Request.Cookies["jwt"]);
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

        [Authorize]
        [HttpGet]
        public IActionResult ProductCreatePage()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View(productDto);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProductUpdatePage(string id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(productDto);
                if (response != null) return RedirectToAction(
                    nameof(Index));
            }
            return View(productDto);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProductDeletePage(string id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);

        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ProductDelete(string id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (response) return RedirectToAction(nameof(Index));
            return View(response);
        }
    }
}
