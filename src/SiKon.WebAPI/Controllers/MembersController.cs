using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SiKon.Shared.Services.Members.Queries.GetAllMembers;
using SiKon.Shared.Services.Members.Queries.GetMember;

namespace SiKon.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public MembersController(ILogger<MembersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<GetAllMembersResponse> GetAllMembersWithDapper()
        {
            GetAllMembersRequest request = new GetAllMembersRequest();

            var response = await Mediator.Send(request);

            return response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<GetMemberResponse> GetMemberByMemberID(int id)
        {
            GetMemberRequest request = new GetMemberRequest
            {
                MemberID = id
            };

            var response = await Mediator.Send(request);

            return response;
        }
    }
}