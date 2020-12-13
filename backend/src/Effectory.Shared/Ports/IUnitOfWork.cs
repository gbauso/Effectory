using System;
using System.Threading.Tasks;

namespace Effectory.Shared.Ports
{
    public interface IUnitOfWork<T>
    {
        Task<T> GetOrCreate(object id, Func<T> objectCreation = null);
        Task Commit();
    }
}
