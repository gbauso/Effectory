using Effectory.Shared.Domain;
using System.Threading.Tasks;

namespace Effectory.Shared.Ports
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        Task<T> Get(object getBy);
        Task<T> Save(T entry);
    }
}
