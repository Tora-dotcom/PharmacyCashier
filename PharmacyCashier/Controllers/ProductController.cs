using Microsoft.AspNetCore.Mvc;
using PharmacyCashier.Models;
using PharmacyCashier.Service;

namespace PharmacyCashier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            await _productService.UpdateProductAsync(id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProducts(string? category, bool? requiresPrescription)
        {
            var filteredProducts = await _productService.FilterProductsAsync(category, requiresPrescription);
            return Ok(filteredProducts);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetProductsPaginated(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var paginatedProducts = await _productService.GetProductsPaginatedAsync(page, pageSize, sortBy, ascending);
            return Ok(paginatedProducts);
        }

        [HttpPost("validate-prescription")]
        public async Task<IActionResult> ValidatePrescription(int productId, int prescriptionId)
        {
            var isValid = await _productService.ValidatePrescriptionAsync(productId, prescriptionId);
            if (!isValid)
                return BadRequest("Invalid prescription for the selected product.");

            return Ok("Prescription is valid.");
        }

    }

}
