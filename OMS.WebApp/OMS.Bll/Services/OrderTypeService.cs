using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.OrderTypes;
using OMS.Common.Exceptions;
using OMS.Dal.Interfaces;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class OrderTypeService : IOrderTypeService
    {
        private readonly IRepository<OrderType> _orderTypeRepository;
        private readonly IMapper _mapper;

        public OrderTypeService(IRepository<OrderType> orderTypeRepository, IMapper mapper)
        {
            _orderTypeRepository = orderTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderTypeDto>> GetOrderTypesAsync()
        {
            var records = await _orderTypeRepository.GetAllAsync();
            if (!records.Any())
            {
                throw new ValidationException(ErrorMessages.NoOrderTypes);
            }
            return _mapper.Map<IEnumerable<OrderTypeDto>>(records);
        }
    }
}
