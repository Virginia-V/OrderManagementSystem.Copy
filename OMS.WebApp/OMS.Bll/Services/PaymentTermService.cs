using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common.Dtos.PaymentTerms;
using OMS.Dal.Interfaces;
using OMS.Domain;
using OMS.Common;
using OMS.Common.Exceptions;

namespace OMS.Bll.Services
{
    public class PaymentTermService : IPaymentTermService
    {
        private readonly IRepository<PaymentTerm> _paymentTermRepository;
        private readonly IMapper _mapper;

        public PaymentTermService(IRepository<PaymentTerm> paymentTermRepository, IMapper mapper)
        {
            _paymentTermRepository = paymentTermRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentTermDto>> GetPaymentTermsAsync()
        {
            var records = await _paymentTermRepository.GetAllAsync();
            if (!records.Any())
            {
                throw new ValidationException(ErrorMessages.NoPaymentTerms);
            }
            return _mapper.Map<IEnumerable<PaymentTermDto>>(records);
        }
    }
}
