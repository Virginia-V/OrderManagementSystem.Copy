using OMS.Common.Dtos.Products;
using OMS.Domain;

namespace OMS.Dal.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductQuantityDto>> GetTopFiveSellingProductsAsync();
    }
}
