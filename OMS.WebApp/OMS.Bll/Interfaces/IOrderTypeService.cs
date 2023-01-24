using OMS.Common.Dtos.OrderTypes;

namespace OMS.Bll.Interfaces
{
    public interface IOrderTypeService
    {
        Task<IEnumerable<OrderTypeDto>> GetOrderTypesAsync();
    }
}
