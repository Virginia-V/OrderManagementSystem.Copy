using OMS.Common.Models.PagedRequest;
using OMS.Dal.Extensions;
using OMS.Dal.Interfaces;
using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OMS.Dal.Repositories
{
    public class InvoiceRepository : EFCoreRepository<Invoice>, IInvoiceRepository
    {
        private string? _searchTerm;
        public InvoiceRepository(OMSDbContext context) : base(context)
        {
        }

        public override async Task<KeyValuePair<int, IEnumerable<Invoice>>> GetPagedDataAsync(PagedRequest pagedRequest)
        {
            _searchTerm = pagedRequest.RequestFilters.Filters.FirstOrDefault()?.Value;
            var query = _context.Invoices.AsQueryable();

            if (!string.IsNullOrEmpty(_searchTerm))
                query = FilterByInvoiceDate(query)
                         .Concat(FilterByInvoiceNumber(query))
                         .Concat(FilterByDiscount(query))
                         .Concat(FilterByShippingAmount(query))
                         .Concat(FilterByPaymentTermsType(query))
                         .Concat(FilterByPaymentStatus(query))
                         .Concat(FilterByInvoicedAmount(query))
                         .Distinct();

            query = OrderBy(pagedRequest.ColumnNameForSorting, pagedRequest.SortDirection, query);
            var total = await query.CountAsync();
            query = query.Paginate(pagedRequest);
            return new KeyValuePair<int, IEnumerable<Invoice>>(total, query);
        }

        private IQueryable<Invoice> FilterByInvoiceDate(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => x.InvoiceDate == GetSearchTermAs<DateTime>(_searchTerm));
        }

        private IQueryable<Invoice> FilterByInvoiceNumber(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => x.InvoiceNumber.Contains(_searchTerm));
        }
        private IQueryable<Invoice> FilterByDiscount(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => x.Discount.DiscountType.Contains(_searchTerm));
        }
        private IQueryable<Invoice> FilterByShippingAmount(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => x.ShippingAmount.ToString().Contains(_searchTerm));
        }
        private IQueryable<Invoice> FilterByPaymentTermsType(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => x.PaymentTerm.PaymentTermsType.Contains(_searchTerm));
        }
        private IQueryable<Invoice> FilterByPaymentStatus(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => x.PaymentStatus.Status.Contains(_searchTerm));
        }
        private IQueryable<Invoice> FilterByInvoicedAmount(IQueryable<Invoice> items)
        {
            return FilterBy(items, x => (x.Order.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum()
                - x.Order.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum() *
                x.Discount.DiscountValue + x.ShippingAmount).ToString().Contains(_searchTerm));
        }

        private static IQueryable<Invoice> FilterBy(IQueryable<Invoice> items, Expression<Func<Invoice, bool>> predicate)
        {
            return items.Where(predicate);
        }

        private IQueryable<Invoice> OrderBy(string columnName, string direction, IQueryable<Invoice> records)
        {
            if (string.IsNullOrEmpty(columnName))
                return records;
            if (columnName == "invoiceDate")
                return direction == "asc" ? records.OrderBy(x => x.InvoiceDate) :
                       records.OrderByDescending(x => x.InvoiceDate);
            if (columnName == "invoiceNumber")
                return direction == "asc" ? records.OrderBy(x => x.InvoiceNumber) :
                       records.OrderByDescending(x => x.InvoiceNumber);
            if (columnName == "discount")
                return direction == "asc" ? records.OrderBy(x => x.Discount.DiscountType) :
                       records.OrderByDescending(x => x.Discount.DiscountType);
            if (columnName == "shippingAmount")
                return direction == "asc" ? records.OrderBy(x => x.ShippingAmount) :
                       records.OrderByDescending(x => x.ShippingAmount);
            if (columnName == "invoicedAmount")
                return direction == "asc" ? records.OrderBy(x => x.Order.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum()
                       - x.Order.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum() * x.Discount.DiscountValue + x.ShippingAmount)
                       : records.OrderByDescending(x => x.Order.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum() -
                       x.Order.OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum() * x.Discount.DiscountValue + x.ShippingAmount);
            if (columnName == "paymentTerm")
                return direction == "asc" ? records.OrderBy(x => x.PaymentTerm.PaymentTermsType) :
                       records.OrderByDescending(x => x.PaymentTerm.PaymentTermsType);
            if (columnName == "paymentStatus")
                return direction == "asc" ? records.OrderBy(x => x.PaymentStatus.Status) :
                       records.OrderByDescending(x => x.PaymentStatus.Status);

            return direction == "asc" ? records.OrderBy(x => x.Id) : records.OrderByDescending(x => x.Id);
        }
    }
}
