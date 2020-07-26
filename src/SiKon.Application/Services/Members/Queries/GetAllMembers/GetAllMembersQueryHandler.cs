using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiKon.Application.Interfaces;
using SiKon.Shared.Services.Members.Queries.GetAllMembers;

namespace SiKon.Application.Services.Members.Queries.GetAllMembers
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersRequest, GetAllMembersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobScheduler _scheduler;

        public GetAllMembersQueryHandler(IUnitOfWork unitOfWork, IBackgroundJobScheduler scheduler)
        {
            _unitOfWork = unitOfWork;
            _scheduler = scheduler;
        }

        public async Task<GetAllMembersResponse> Handle(GetAllMembersRequest request, CancellationToken cancellationToken)
        {
            var members = (await _unitOfWork.Members.GetAll()).ToList();

            await _scheduler.RegisterToScheduler("Get All Members nich!");

            GetAllMembersResponse response = new GetAllMembersResponse();

            foreach (var member in members)
            {
                MemberDTO memberDTO = new MemberDTO
                {
                    MemberID = member.MemberID,
                    Username = member.Username,
                    FullName = member.FullName
                };

                response.Members.Add(memberDTO);
            }

            return response;
        }
    }
}