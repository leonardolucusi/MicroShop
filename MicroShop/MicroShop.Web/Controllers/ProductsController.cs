using MicroShop.Web.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Web.Controllers
{
 
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductAPI");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/products");

            if (response.IsSuccessStatusCode)
            {
                List<ProductDTO> products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                return View(products);
            }
            return View("Error");
        }
    }
}
