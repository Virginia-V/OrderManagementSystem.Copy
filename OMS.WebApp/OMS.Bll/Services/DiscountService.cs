using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.Discounts;
using OMS.Common.Exceptions;
using OMS.Dal.Interfaces;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<Discount> _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IRepository<Discount> discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DiscountDto>> GetDiscountsAsync()
        {
            var records = await _discountRepository.GetAllAsync();
            if (!records.Any())
            {
                throw new ValidationException(ErrorMessages.NoDiscounts);
            }
            return _mapper.Map<IEnumerable<DiscountDto>>(records);
        }
    }
}
