using System.Collections.Generic;

namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public class GetAllTCPEndpointsResponse
    {
        public IList<TCPEndpointDTO> TCPEndpoints { get; set; }

        public GetAllTCPEndpointsResponse()
        {
            this.TCPEndpoints = new List<TCPEndpointDTO>();
        }
    }
}