using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MicroShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _productApiClient;
        
        private readonly IProductService _productService;
        public ProductsController(IHttpClientFactory httpClientFactory, IProductService productService)
        {
            _productApiClient = httpClientFactory.CreateClient("ProductAPI");
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _productApiClient.GetAsync("api/products");
        
            if (response.IsSuccessStatusCode)
            {
                List<ProductDTO> products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
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
