using KSAA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Domain.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity
    {
        public IQueryable<T> GetAllAsync();
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);

        public Task<T> FindById(long id);

        public Task<IQueryable<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        public Task<T> AddAsync(T entity);

        public Task<T> UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
