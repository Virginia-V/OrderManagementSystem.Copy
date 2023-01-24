using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common.Dtos.Customers;
using OMS.Dal.Interfaces;
using OMS.Domain;
using OMS.Common;
using OMS.Common.Models.PagedRequest;
using OMS.Common.Exceptions;

namespace OMS.Bll.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            var result = _mapper.Map<UpdateCustomerDto>(customer);
            return result;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var records = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(records);
        }

        public async Task<IEnumerable<CustomerDto>> GetNewCustomersAsync()
        {
            var records = await _customerRepository.GetNewCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(records);
        }

        public async Task AddCustomerAsync(CreateCustomerDto dto)
        {
            var customers = await _customerRepository.GetWhereAsync(c => c.CompanyName == dto.CompanyName);
            var customerWithSameName = customers.FirstOrDefault();
            if (customerWithSameName != null)
            {
                throw new ValidationException(ErrorMessages.CustomerAlreadyExists);
            }
            var customer = _mapper.Map<Customer>(dto);
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new ValidationException(ErrorMessages.NoCustomerForUpdate);
            }
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                customer.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                customer.LastName = dto.LastName;

            if (!string.IsNullOrWhiteSpace(dto.CompanyName))
                customer.CompanyName = dto.CompanyName;

            if (dto.CustomerTypeId.HasValue)
                customer.CustomerTypeId = dto.CustomerTypeId.Value;

            if (!string.IsNullOrWhiteSpace(dto.BillingAddress))
                customer.BillingAddress = dto.BillingAddress;

            if (!string.IsNullOrWhiteSpace(dto.ShippingAddress))
                customer.ShippingAddress = dto.ShippingAddress;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                customer.Email = dto.Email;

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new ValidationException(ErrorMessages.NoCustomerForDelete);
            }
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task<PaginatedResult<CustomerDto>> GetPagedCustomersAsync(PagedRequest pagedRequest)
        {
            var dbItems = await _customerRepository.GetPagedDataAsync(pagedRequest);
            return new PaginatedResult<CustomerDto>
            {
                Items = _mapper.Map<IEnumerable<CustomerDto>>(dbItems.Value).ToList(),
                PageIndex = pagedRequest.PageIndex,
                PageSize = pagedRequest.PageSize,
                Total = dbItems.Key
            };
        }
    }
}
