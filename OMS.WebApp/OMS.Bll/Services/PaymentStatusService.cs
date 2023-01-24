using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.PaymentStatuses;
using OMS.Common.Exceptions;
using OMS.Dal.Interfaces;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly IRepository<PaymentStatus> _paymentStatusRepository;
        private readonly IMapper _mapper;

        public PaymentStatusService(IRepository<PaymentStatus> paymentStatusRepository, IMapper mapper)
        {
            _paymentStatusRepository = paymentStatusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentStatusDto>> GetPaymentStatusesAsync()
        {
            var records = await _paymentStatusRepository.GetAllAsync();
            if (!records.Any())
            {
                throw new ValidationException(ErrorMessages.NoPaymentStatuses);
            }
            return _mapper.Map<IEnumerable<PaymentStatusDto>>(records);
        }
    }
}
