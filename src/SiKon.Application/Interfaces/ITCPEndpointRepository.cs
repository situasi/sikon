using System.Collections.Generic;
using System.Threading.Tasks;
using SiKon.Domain.Entities;

namespace SiKon.Application.Interfaces
{
    public interface ITCPEndpointRepository : IGenericRepository<TCPEndpoint>
    {
        Task<IEnumerable<TCPEndpoint>> GetAllIncludeMember();
    }
}