using MicroShop.Web.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var jwtToken = Request.Cookies["jwt"];
            //if (string.IsNullOrEmpty(jwtToken)) return RedirectToAction("Login");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

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
