using Effectory.Core.Model;
using Effectory.Shared.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Effectory.Infra.Repository.Interfaces
{
    public interface IQuestionnaireRepository : IRepository<Questionnaire>
    {
        Task<ICollection<Questionnaire>> GetAllSimple();
    }
}
