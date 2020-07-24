using System.Collections.Generic;

namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public class GetAllTCPEndpointResponse
    {
        public IList<TCPEndpointDTO> TCPEndpoints { get; set; }

        public GetAllTCPEndpointResponse()
        {
            this.TCPEndpoints = new List<TCPEndpointDTO>();
        }
    }
}