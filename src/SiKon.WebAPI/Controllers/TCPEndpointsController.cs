using System.Threading.Tasks;
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
        private readonly IUnitOfWork _unitOfWork;

        public TCPEndpointsController(ISiKonDBContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("cara1")]
        public async Task<GetAllTCPEndpointResponse> GetAllTCPEndpoints1()
        {
            return await GetAllTCPEndpointsQuery.Handle1(_context);
        }

        [HttpGet]
        [Route("cara2")]
        public async Task<GetAllTCPEndpointResponse> GetAllTCPEndpoints2()
        {
            return await GetAllTCPEndpointsQuery.Handle2(_unitOfWork);
        }
    }
}