using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiKon.Application.Interfaces;
using AutoMapper;
using SiKon.Shared.Services.Members.Queries.GetMember;

namespace SiKon.Application.Services.Members.Queries.GetMember
{
    public class GetMemberQueryHandler : IRequestHandler<GetMemberRequest, GetMemberResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMemberQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetMemberResponse> Handle(GetMemberRequest request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Members.Get(request.MemberID);

            var response = _mapper.Map<GetMemberResponse>(member);

            return response;
        }
    }
}