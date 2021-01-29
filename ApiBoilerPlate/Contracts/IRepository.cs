using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<long> CreateAsync(T entity);

    }
}
