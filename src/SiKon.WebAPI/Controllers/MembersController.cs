using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiKon.Application.Interfaces;
using SiKon.Application.Services.Members.Queries.GetAllMembers;

namespace SiKon.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ISiKonDBContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public MembersController(ISiKonDBContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("efcore")]
        public async Task<GetAllMembersResponse> GetAllMembersWithEFCore()
        {
            return await GetAllMembersQuery.HandleWithEFCore(_context);
        }

        [HttpGet]
        [Route("dapper")]
        public async Task<GetAllMembersResponse> GetAllMembersWithDapper()
        {
            return await GetAllMembersQuery.HandleWithDapper(_unitOfWork);
        }
    }
}