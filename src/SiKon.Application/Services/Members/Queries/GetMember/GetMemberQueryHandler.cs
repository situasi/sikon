using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Services.Members.Queries.GetMember
{
    public class GetMemberQueryHandler : IRequestHandler<GetMemberRequest, GetMemberResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMemberQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetMemberResponse> Handle(GetMemberRequest request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Members.Get(request.MemberID);

            GetMemberResponse response = new GetMemberResponse
            {
                MemberID = member.MemberID,
                Username = member.Username,
                FullName = member.FullName
            };

            return response;
        }
    }
}