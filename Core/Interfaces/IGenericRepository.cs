using Core.Entities;
using Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetEntityWithSpecAsync(IBaseSpecification<T> spec);

        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(IBaseSpecification<T> spec);
        Task<int> CountAsync(IBaseSpecification<T> spec);
    }
}
