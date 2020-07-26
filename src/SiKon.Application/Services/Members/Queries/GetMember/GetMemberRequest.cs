using MediatR;

namespace SiKon.Application.Services.Members.Queries.GetMember
{
    public class GetMemberRequest : IRequest<GetMemberResponse>
    {
        public int MemberID { get; set; }
    }
}