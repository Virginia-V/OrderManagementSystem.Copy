using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Invoices;
using OMS.Dal.Interfaces;
using OMS.Domain;
using OMS.Common;
using OMS.Common.Models.PagedRequest;
using OMS.Common.Exceptions;

namespace OMS.Bll.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private const int STATUS_UNPAID = 2;

        public InvoiceService(IInvoiceRepository invoiceRepository,
            IOrderRepository orderRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;

        }
        public async Task AddInvoiceAsync(CreateInvoiceDto dto)
        {
            var invoices = await _invoiceRepository.GetWhereAsync(i => i.InvoiceNumber == dto.InvoiceNumber);
            var invoiceWithSameNum = invoices.SingleOrDefault();
            if (invoiceWithSameNum != null)
            {
                throw new ValidationException(ErrorMessages.InvoiceAlreadyExists);
            }
            var ordersWithExistingPONum = await _orderRepository.GetWhereAsync(o => o.PurchaseOrderNumber == dto.PurchaseOrderNumber);
            var order = ordersWithExistingPONum.SingleOrDefault();
            if (order == null)
            {
                throw new ValidationException(ErrorMessages.OrderDoesNotExist);
            }
            var invoice = _mapper.Map<Invoice>(dto);
            invoice.Order = order;
            await _invoiceRepository.AddAsync(invoice);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new ValidationException(ErrorMessages.NoInvoiceForDelete);
            }
            await _invoiceRepository.DeleteAsync(invoice);
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            var result = _mapper.Map<InvoiceDto>(invoice);
            return result;
        }

        public async Task<IEnumerable<InvoiceDto>> GetInvoicesAsync()
        {
            var records = await _invoiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(records);
        }

        public async Task<IEnumerable<OpenInvoiceDto>> GetOpenInvoicesAsync()
        {
            var records = await _invoiceRepository.GetWhereAsync(i => i.PaymentStatusId == STATUS_UNPAID);
            return _mapper.Map<IEnumerable<OpenInvoiceDto>>(records);
        }
        public async Task<IEnumerable<OpenInvoiceDto>> GetOverdueInvoicesAsync()
        {
            var records = await _invoiceRepository.GetWhereAsync(i => i.PaymentStatusId == STATUS_UNPAID && DateTime.Now > i.DueDate);
            return _mapper.Map<IEnumerable<OpenInvoiceDto>>(records.Take(5));
        }

        public async Task<PaginatedResult<InvoiceDto>> GetPagedInvoicesAsync(PagedRequest pagedRequest)
        {
            var dbItems = await _invoiceRepository.GetPagedDataAsync(pagedRequest);
            return new PaginatedResult<InvoiceDto>
            {
                Items = _mapper.Map<IEnumerable<InvoiceDto>>(dbItems.Value).ToList(),
                PageIndex = pagedRequest.PageIndex,
                PageSize = pagedRequest.PageSize,
                Total = dbItems.Key
            };
        }
    }
}
