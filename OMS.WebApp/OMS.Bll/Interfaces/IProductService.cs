using OMS.Common.Dtos.Products;
using OMS.Common.Models.PagedRequest;

namespace OMS.Bll.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<PaginatedResult<ProductDto>> GetPagedProductsAsync(PagedRequest pagedRequest);
        Task<UpdateProductDto> GetProductByIdAsync(int id);
        Task CreateProductAsync(CreateProductDto dto);
        Task UpdateProductAsync(int id, UpdateProductDto dto);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductQuantityDto>> GetTopFiveSellingProductsAsync();
    }
}
