using OMS.Domain;

namespace OMS.Dal.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetNewCustomersAsync();
    }
}
