using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public static class GetAllTCPEndpointsQuery
    {
        public static async Task<GetAllTCPEndpointResponse> Handle1(ISiKonDBContext database)
        {
            var tcpEndpoints = await database.TCPEndpoints.ToListAsync();

            GetAllTCPEndpointResponse response = new GetAllTCPEndpointResponse();

            foreach (var tcpEndpoint in tcpEndpoints)
            {
                TCPEndpointDTO tcpEndpointDTO = new TCPEndpointDTO
                {
                    FriendlyName = tcpEndpoint.FriendlyName,
                    TargetAddress = tcpEndpoint.TargetAddress,
                    PortNumber = tcpEndpoint.PortNumber
                };

                response.TCPEndpoints.Add(tcpEndpointDTO);
            }

            return response;
        }

        public static async Task<GetAllTCPEndpointResponse> Handle2(IUnitOfWork _unitOfWork)
        {
            var tcpEndpoints = (await _unitOfWork.TCPEndpoints.GetAll()).ToList();

            GetAllTCPEndpointResponse response = new GetAllTCPEndpointResponse();

            foreach (var tcpEndpoint in tcpEndpoints)
            {
                TCPEndpointDTO tcpEndpointDTO = new TCPEndpointDTO
                {
                    FriendlyName = tcpEndpoint.FriendlyName,
                    TargetAddress = tcpEndpoint.TargetAddress,
                    PortNumber = tcpEndpoint.PortNumber
                };

                response.TCPEndpoints.Add(tcpEndpointDTO);
            }

            return response;
        }
    }
}