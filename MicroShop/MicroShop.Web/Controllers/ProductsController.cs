using MicroShop.Web.Models.DTOs;
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
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/products");

            if (response.IsSuccessStatusCode)
            {
                List<ProductDTO> products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                return View(products);
            }
            else
            {
                // Lidar com o erro de acordo com o status code
                return View("Error");
            }
        }
    }
}
