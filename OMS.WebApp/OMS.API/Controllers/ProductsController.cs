using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Products;
using OMS.Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace OMS.API.Controllers
{
    [Route("api/products")]
    public class ProductsController : AppBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, 
            ILogger<ProductsController> logger, 
            IStringLocalizer<Resource> stringLocalizer) : base(logger, stringLocalizer)
        {
            _productService = productService;
        }

        [HttpGet]
        public Task<IActionResult> GetProductsAsync()
        {
            return HandleAsync(() => _productService.GetProductsAsync());
        }

        [HttpPost("paginated-search")]
        public Task<IActionResult> GetPagedProductsAsync(PagedRequest pagedRequest)
        {
            return HandleAsync(() => _productService.GetPagedProductsAsync(pagedRequest));
        }

        [HttpGet("topProducts")]
        public Task<IActionResult> GetTopProductsAsync()
        {
            return HandleAsync(() => _productService.GetTopFiveSellingProductsAsync());
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetProductAsync(int id)
        {
            return HandleAsync(() => _productService.GetProductByIdAsync(id));
        }

        [HttpPost]
        public Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto productDto)
        {
            return HandleAsync(() => _productService.CreateProductAsync(productDto));
        }

        [HttpPatch("{id}")]
        public Task<IActionResult> UpdateProductAsync(int id, [FromBody] UpdateProductDto productDto)
        {
            return HandleAsync(() => _productService.UpdateProductAsync(id, productDto));
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteProductAsync(int id)
        {
            return HandleAsync(() => _productService.DeleteProductAsync(id));
        }
    }
}
