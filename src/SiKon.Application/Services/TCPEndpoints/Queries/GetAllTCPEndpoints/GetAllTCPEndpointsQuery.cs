using System.Linq;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public static class GetAllTCPEndpointsQuery
    {
        public static GetAllTCPEndpointResponse Handle(ISiKonDBContext database)
        {
            var tcpEndpoints = database.TCPEndpoints.ToList();

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