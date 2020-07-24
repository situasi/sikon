using Microsoft.AspNetCore.Mvc;
using SiKon.Application.Interfaces;
using SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints;

namespace IPMAN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TCPEndpointsController : ControllerBase
    {
        private readonly ISiKonDBContext _context;

        public TCPEndpointsController(ISiKonDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public GetAllTCPEndpointResponse GetAllTCPEndpoints()
        {
            return GetAllTCPEndpointsQuery.Handle(_context);
        }
    }
}