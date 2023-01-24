using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.CustomerTypes;
using OMS.Common.Exceptions;
using OMS.Dal.Interfaces;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class CustomerTypeService : ICustomerTypeService
    {
        private readonly IRepository<CustomerType> _customerTypeRepository;
        private readonly IMapper _mapper;

        public CustomerTypeService(IRepository<CustomerType> customerTypeRepository, IMapper mapper)
        {
            _customerTypeRepository = customerTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerTypeDto>> GetCustomerTypesAsync()
        {
            var records = await _customerTypeRepository.GetAllAsync();
            if (!records.Any())
            {
                throw new ValidationException(ErrorMessages.NoCustomerTypes);
            }
            return _mapper.Map<IEnumerable<CustomerTypeDto>>(records);
        }
    }
}
