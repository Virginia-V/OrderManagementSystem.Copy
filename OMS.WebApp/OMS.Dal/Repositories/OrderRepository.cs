using OMS.Common.Dtos.Orders;
using OMS.Common.Models.PagedRequest;
using OMS.Dal.Extensions;
using OMS.Dal.Interfaces;
using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OMS.Dal.Repositories
{
    public class OrderRepository : EFCoreRepository<Order>, IOrderRepository
    {
        private string? _searchTerm;
        public OrderRepository(OMSDbContext context) : base(context)
        {
        }

        public override async Task<KeyValuePair<int, IEnumerable<Order>>> GetPagedDataAsync(PagedRequest pagedRequest)
        {
            _searchTerm = pagedRequest.RequestFilters.Filters.FirstOrDefault()?.Value;
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(_searchTerm))
                query = FilterByOrderedAt(query)
                          .Concat(FilterByPurchaseOrder(query))
                          .Concat(FilterByCustomer(query))
                          .Concat(FilterByOrderType(query))
                          .Concat(FilterByTotalAmount(query))
                          .Distinct();

            query = OrderBy(pagedRequest.ColumnNameForSorting, pagedRequest.SortDirection, query);
            var total = await query.CountAsync();
            query = query.Paginate(pagedRequest);
            return new KeyValuePair<int, IEnumerable<Order>>(total, query);
        }

        private IQueryable<Order> FilterByOrderedAt(IQueryable<Order> items)
        {
            return FilterBy(items, x => x.OrderedAt == GetSearchTermAs<DateTime>(_searchTerm));
        }
        private IQueryable<Order> FilterByPurchaseOrder(IQueryable<Order> items)
        {
            return FilterBy(items, x => x.PurchaseOrderNumber.Contains(_searchTerm));
        }
        private IQueryable<Order> FilterByCustomer(IQueryable<Order> items)
        {
            return FilterBy(items, x => x.Customer.CompanyName.Contains(_searchTerm));
        }
        private IQueryable<Order> FilterByOrderType(IQueryable<Order> items)
        {
            return FilterBy(items, x => x.OrderType.Type.Contains(_searchTerm));
        }
        private IQueryable<Order> FilterByTotalAmount(IQueryable<Order> items)
        {
            return FilterBy(items, x => x.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum()
                .ToString().Contains(_searchTerm));
        }

        private static IQueryable<Order> FilterBy(IQueryable<Order> items, Expression<Func<Order, bool>> predicate)
        {
            return items.Where(predicate);
        }
        private IQueryable<Order> OrderBy(string columnName, string direction, IQueryable<Order> records)
        {
            if (string.IsNullOrEmpty(columnName))
                return records;
            if (columnName == "orderedAt")
                return direction == "asc" ? records.OrderBy(x => x.OrderedAt) :
                       records.OrderByDescending(x => x.OrderedAt);
            if (columnName == "purchaseOrderNumber")
                return direction == "asc" ? records.OrderBy(x => x.PurchaseOrderNumber) :
                       records.OrderByDescending(x => x.PurchaseOrderNumber);
            if (columnName == "customer")
                return direction == "asc" ? records.OrderBy(x => x.Customer.CompanyName) :
                       records.OrderByDescending(x => x.Customer.CompanyName);
            if (columnName == "totalOrderedAmount")
                return direction == "asc" ?
                       records.OrderBy(x => x.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum()) :
                       records.OrderByDescending(x => x.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum());
            if (columnName == "orderType")
                return direction == "asc" ? records.OrderBy(x => x.OrderType.Type) :
                       records.OrderByDescending(x => x.OrderType.Type);
            return direction == "asc" ? records.OrderBy(x => x.Id) : records.OrderByDescending(x => x.Id);
        }
        public async Task<IEnumerable<SalesByCategoryDto>> GetSalesByProductCategoryAsync()
        {
            var records = await _context.OrderedProducts.GroupBy(x => x.Product.Category.CategoryName)
            .Select(x => new { Category = x.Key, Sales = x.Sum(y => y.Quantity * y.Product.ProductPrice) }).ToListAsync();

            return records.Select(x => new SalesByCategoryDto
            {
                Category = x.Category,
                Sales = x.Sales
            });
        }
    }
}
