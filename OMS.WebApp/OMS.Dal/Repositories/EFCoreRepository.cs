using OMS.Common.Models.PagedRequest;
using OMS.Dal.Extensions;
using OMS.Dal.Interfaces;
using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OMS.Dal.Repositories
{
    public class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly OMSDbContext _context;

        public EFCoreRepository(OMSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task<KeyValuePair<int, IEnumerable<TEntity>>> GetPagedDataAsync(PagedRequest pagedRequest)
        {
            var query = _context.Set<TEntity>().ApplyFilters(pagedRequest).Sort(pagedRequest);
            var total = await query.CountAsync();
            query = query.Paginate(pagedRequest);
            return new KeyValuePair<int, IEnumerable<TEntity>>(total, query);
        }
        protected static T GetSearchTermAs<T>(string term)
        {
            if (string.IsNullOrEmpty(term))
                return default;
            object parsedResult = null;

            if (typeof(T) == typeof(DateTime))
            {
                DateTime.TryParse(term, out DateTime result);
                parsedResult = result;
            }
            return (T)parsedResult;
        }
    }
}
