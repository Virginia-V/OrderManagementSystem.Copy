using OMS.Common.Models.PagedRequest;
using OMS.Dal.Extensions;
using OMS.Dal.Interfaces;
using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OMS.Dal.Repositories
{
    public class CustomerRepository : EFCoreRepository<Customer>, ICustomerRepository
    {
        private string? _searchTerm;
        private const int TYPE_FIRST_ORDER = 1;
        public CustomerRepository(OMSDbContext context) : base(context)
        {
        }

        public override async Task<KeyValuePair<int, IEnumerable<Customer>>> GetPagedDataAsync(PagedRequest pagedRequest)
        {
            _searchTerm = pagedRequest.RequestFilters.Filters.FirstOrDefault()?.Value;
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(_searchTerm))
                query = FilterByFirstName(query)
                           .Concat(FilterByLastName(query))
                           .Concat(FilterByCompanyName(query))
                           .Concat(FilterByCustomerType(query))
                           .Distinct();

            query = OrderBy(pagedRequest.ColumnNameForSorting, pagedRequest.SortDirection, query);
            var total = await query.CountAsync();
            query = query.Paginate(pagedRequest);
            return new KeyValuePair<int, IEnumerable<Customer>>(total, query);
        }

        private IQueryable<Customer> FilterByFirstName(IQueryable<Customer> items)
        {
            return FilterBy(items, x => x.FirstName.Contains(_searchTerm));
        }
        private IQueryable<Customer> FilterByLastName(IQueryable<Customer> items)
        {
            return FilterBy(items, x => x.LastName.Contains(_searchTerm));
        }
        private IQueryable<Customer> FilterByCompanyName(IQueryable<Customer> items)
        {
            return FilterBy(items, x => x.CompanyName.Contains(_searchTerm));
        }
        private IQueryable<Customer> FilterByCustomerType(IQueryable<Customer> items)
        {
            return FilterBy(items, x => x.CustomerType.Type.Contains(_searchTerm));
        }

        private static IQueryable<Customer> FilterBy(IQueryable<Customer> items, Expression<Func<Customer, bool>> predicate)
        {
            return items.Where(predicate);
        }

        private IQueryable<Customer> OrderBy(string columnName, string direction, IQueryable<Customer> records)
        {
            if (string.IsNullOrEmpty(columnName))
                return records;
            if (columnName == "firstName")
                return direction == "asc" ? records.OrderBy(x => x.FirstName) :
                       records.OrderByDescending(x => x.FirstName);
            if (columnName == "lastName")
                return direction == "asc" ? records.OrderBy(x => x.LastName) :
                       records.OrderByDescending(x => x.LastName);
            if (columnName == "companyName")
                return direction == "asc" ? records.OrderBy(x => x.CompanyName) :
                       records.OrderByDescending(x => x.CompanyName);
            if (columnName == "customerType")
                return direction == "asc" ? records.OrderBy(x => x.CustomerType.Type) :
                       records.OrderByDescending(x => x.CustomerType.Type);

            return direction == "asc" ? records.OrderBy(x => x.Id) : records.OrderByDescending(x => x.Id);
        }

        public async Task<IEnumerable<Customer>> GetNewCustomersAsync()
        {
            var records = await _context.Orders.Where(x => x.OrderTypeId == TYPE_FIRST_ORDER)
                          .Select(x => x.Customer).ToListAsync();
            return records;
        }

    }
}
