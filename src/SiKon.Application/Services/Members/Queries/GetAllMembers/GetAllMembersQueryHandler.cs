using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Services.Members.Queries.GetAllMembers
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersRequest, GetAllMembersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMembersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllMembersResponse> Handle(GetAllMembersRequest request, CancellationToken cancellationToken)
        {
            var members = (await _unitOfWork.Members.GetAll()).ToList();

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