using OMS.Common.Dtos.CustomerTypes;

namespace OMS.Bll.Interfaces
{
    public interface ICustomerTypeService
    {
        Task<IEnumerable<CustomerTypeDto>> GetCustomerTypesAsync();
    }
}
