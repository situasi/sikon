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
        [Route("efcore")]
        public async Task<GetAllTCPEndpointsResponse> GetAllTCPEndpointsWithEFCore()
        {
            return await GetAllTCPEndpointsQuery.HandleWithEFCore(_context);
        }

        [HttpGet]
        [Route("dapper")]
        public async Task<GetAllTCPEndpointsResponse> GetAllTCPEndpointsWithDapper()
        {
            return await GetAllTCPEndpointsQuery.HandleWithDapper(_unitOfWork);
        }
    }
}