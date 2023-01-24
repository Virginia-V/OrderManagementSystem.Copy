using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.Orders;
using OMS.Common.Exceptions;
using OMS.Common.Models.PagedRequest;
using OMS.Dal.Interfaces;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var records = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(records);
        }

        public async Task AddOrderAsync(CreateOrderDto dto)
        {
            var orders = await _orderRepository.GetWhereAsync(o => o.PurchaseOrderNumber == dto.PurchaseOrderNumber);
            var orderWithSamePONum = orders.SingleOrDefault();
            if (orderWithSamePONum != null)
            {
                throw new ValidationException(ErrorMessages.OrderAlreadyExists);
            }
            if (dto.OrderedProducts.Select(x => x.ProductId).Count()
                != dto.OrderedProducts.Select(x => x.ProductId).Distinct().Count())
            {
                throw new ValidationException(ErrorMessages.SameProductError);
            }
            var order = _mapper.Map<Order>(dto);
            await _orderRepository.AddAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new ValidationException(ErrorMessages.NoOrderForDelete);
            }
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<IEnumerable<OrderDto>> GetTodayOrdersAsync()
        {
            var currentOrders = await _orderRepository.GetWhereAsync(x => x.OrderedAt.Date == DateTime.Now.Date);
            return _mapper.Map<IEnumerable<OrderDto>>(currentOrders);
        }

        public async Task<IEnumerable<SalesByCategoryDto>> GetSalesByProductCategoryAsync()
        {
            var records = await _orderRepository.GetSalesByProductCategoryAsync();
            return records;
        }

        public async Task<PaginatedResult<OrderDto>> GetPagedOrdersAsync(PagedRequest pagedRequest)
        {
            var dbItems = await _orderRepository.GetPagedDataAsync(pagedRequest);
            return new PaginatedResult<OrderDto>
            {
                Items = _mapper.Map<IEnumerable<OrderDto>>(dbItems.Value).ToList(),
                PageIndex = pagedRequest.PageIndex,
                PageSize = pagedRequest.PageSize,
                Total = dbItems.Key
            };
        }
    }
}


