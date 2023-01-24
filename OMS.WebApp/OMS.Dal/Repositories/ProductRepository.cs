using Microsoft.EntityFrameworkCore;
using OMS.Common.Dtos.Products;
using OMS.Common.Models.PagedRequest;
using OMS.Dal.Extensions;
using OMS.Dal.Interfaces;
using OMS.Domain;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace OMS.Dal.Repositories
{
    public class ProductRepository : EFCoreRepository<Product>, IProductRepository
    {
        private string? _searchTerm;
        public ProductRepository(OMSDbContext context) : base(context)
        {
        }

        public override async Task<KeyValuePair<int, IEnumerable<Product>>> GetPagedDataAsync(PagedRequest pagedRequest)
        {
            _searchTerm = pagedRequest.RequestFilters.Filters.FirstOrDefault()?.Value;
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(_searchTerm))
                query = FilterByProductSKU(query)
                            .Concat(FilterByProductName(query))
                            .Concat(FilterByCategory(query))
                            .Concat(FilterByProductPrice(query))
                            .Distinct();

            query = OrderBy(pagedRequest.ColumnNameForSorting, pagedRequest.SortDirection, query);
            var total = await query.CountAsync();
            query = query.Paginate(pagedRequest);
            return new KeyValuePair<int, IEnumerable<Product>>(total, query);
        }

        private IQueryable<Product> FilterByProductSKU(IQueryable<Product> items)
        {
            return FilterBy(items, x => x.ProductSKU.Contains(_searchTerm));
        }
        private IQueryable<Product> FilterByProductName(IQueryable<Product> items)
        {
            return FilterBy(items, x => x.ProductName.Contains(_searchTerm));
        }
        private IQueryable<Product> FilterByCategory(IQueryable<Product> items)
        {
            return FilterBy(items, x => x.Category.CategoryName.Contains(_searchTerm));
        }
        private IQueryable<Product> FilterByProductPrice(IQueryable<Product> items)
        {
            return FilterBy(items, x => x.ProductPrice.ToString().Contains(_searchTerm));
        }

        private static IQueryable<Product> FilterBy(IQueryable<Product> items, Expression<Func<Product, bool>> predicate)
        {
            return items.Where(predicate);
        }
        private IQueryable<Product> OrderBy(string columnName, string direction, IQueryable<Product> records)
        {
            if (string.IsNullOrEmpty(columnName))
                return records;
            if (columnName == "productSKU")
                return direction == "asc" ? records.OrderBy(x => x.ProductSKU) :
                       records.OrderByDescending(x => x.ProductSKU);
            if (columnName == "productName")
                return direction == "asc" ? records.OrderBy(x => x.ProductName) :
                       records.OrderByDescending(x => x.ProductName);
            if (columnName == "category")
                return direction == "asc" ? records.OrderBy(x => x.Category.CategoryName) :
                       records.OrderByDescending(x => x.Category.CategoryName);
            if (columnName == "productPrice")
                return direction == "asc" ? records.OrderBy(x => x.ProductPrice) :
                       records.OrderByDescending(x => x.ProductPrice);

            return direction == "asc" ? records.OrderBy(x => x.Id) : records.OrderByDescending(x => x.Id);
        }

        public async Task<IEnumerable<ProductQuantityDto>> GetTopFiveSellingProductsAsync()
        {
            var records = await _context.OrderedProducts.GroupBy(x => x.Product.ProductSKU)
                      .Select(x => new { Product = x.Key, TotalQuantity = x.Sum(p => p.Quantity) })
                      .OrderByDescending(x => x.TotalQuantity).Take(5).ToListAsync();

            return records.Select(x => new ProductQuantityDto
            {
                ProductSKU = x.Product,
                Quantity = x.TotalQuantity,
            });
        }
    }
}
