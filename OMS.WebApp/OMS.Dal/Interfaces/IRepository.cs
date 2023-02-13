using OMS.Common.Models.PagedRequest;
using OMS.Domain;
using System.Linq.Expressions;

namespace OMS.Dal.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<KeyValuePair<int, IEnumerable<TEntity>>> GetPagedDataAsync(PagedRequest request);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
