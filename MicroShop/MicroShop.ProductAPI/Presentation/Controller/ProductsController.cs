using MicroShop.ProductAPI.Application.Interfaces;
using MicroShop.ProductAPI.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.ProductAPI.API.Controller
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(string id)
        {
            try
            {
                if (!Ulid.TryParse(id, out var productId))
                {
                    return BadRequest("Invalid product ID format.");
                }

                var product = await _productService.GetProductByIdAsync(productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductDTO>> CreateProduct([FromBody] CreateProductDTO productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest("Product data is null.");
                }

                var createdProduct = await _productService.CreateProductAsync(productDto);
                return CreatedAtAction(nameof(GetProducts), new { id = createdProduct.Id.ToString() }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(string id, [FromBody] ProductDTO productDto)
        {
            try
            {
                if (!Ulid.TryParse(id, out var productId))
                {
                    return BadRequest("Invalid product ID format.");
                }

                if (productDto == null)
                {
                    return BadRequest("Product data is null.");
                }

                productDto.Id = productId;

                await _productService.UpdateProductAsync(productDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            try
            {
                if (!Ulid.TryParse(id, out var productId))
                {
                    return BadRequest("Invalid product ID format.");
                }
                await _productService.DeleteProductAsync(productId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

