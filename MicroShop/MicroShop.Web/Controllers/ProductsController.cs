using Azure;
using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace MicroShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IProductService _productService;
        public ProductsController(IHttpClientFactory httpClientFactory, IProductService productService)
        {
            _httpClient = httpClientFactory.CreateClient("ProductAPI");
            _productService = productService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var jwtToken = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(jwtToken)) return RedirectToAction("Login");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await _httpClient.GetAsync("api/products");

            if (response.IsSuccessStatusCode)
            {
                List<ProductDTO> products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                return View(products);
            }
            return View("Error");
        }
        public async Task<IActionResult> ProductUpdatePage(string id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDTO productDto)
        {
            if(ModelState.IsValid) 
            {
                var response = await _productService.UpdateProduct(productDto);
                if (response != null) return RedirectToAction(
                    nameof(Index));
            }
            return View(productDto);
        }
    }
}
