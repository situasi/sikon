using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public static class GetAllTCPEndpointsQuery
    {
        public static async Task<GetAllTCPEndpointsResponse> HandleWithEFCore(ISiKonDBContext database)
        {
            var tcpEndpoints = await database.TCPEndpoints
                .Include(x => x.Member)
                .ToListAsync();

            GetAllTCPEndpointsResponse response = new GetAllTCPEndpointsResponse();

            foreach (var tcpEndpoint in tcpEndpoints)
            {
                TCPEndpointDTO tcpEndpointDTO = new TCPEndpointDTO
                {
                    MemberID = tcpEndpoint.Member.MemberID,
                    FriendlyName = tcpEndpoint.FriendlyName,
                    TargetAddress = tcpEndpoint.TargetAddress,
                    PortNumber = tcpEndpoint.PortNumber,
                    MemberUsername = tcpEndpoint.Member.Username,
                    MemberFullName = tcpEndpoint.Member.FullName
                };

                response.TCPEndpoints.Add(tcpEndpointDTO);
            }

            return response;
        }

        public static async Task<GetAllTCPEndpointsResponse> HandleWithDapper(IUnitOfWork _unitOfWork)
        {
            var tcpEndpoints = (await _unitOfWork.TCPEndpoints.GetAllIncludeMember()).ToList();

            GetAllTCPEndpointsResponse response = new GetAllTCPEndpointsResponse();

            foreach (var tcpEndpoint in tcpEndpoints)
            {
                TCPEndpointDTO tcpEndpointDTO = new TCPEndpointDTO
                {
                    MemberID = tcpEndpoint.Member.MemberID,
                    FriendlyName = tcpEndpoint.FriendlyName,
                    TargetAddress = tcpEndpoint.TargetAddress,
                    PortNumber = tcpEndpoint.PortNumber,
                    MemberUsername = tcpEndpoint.Member.Username,
                    MemberFullName = tcpEndpoint.Member.FullName
                };

                response.TCPEndpoints.Add(tcpEndpointDTO);
            }

            return response;
        }
    }
}