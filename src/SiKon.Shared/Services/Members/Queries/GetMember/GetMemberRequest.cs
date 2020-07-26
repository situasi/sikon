using MediatR;

namespace SiKon.Shared.Services.Members.Queries.GetMember
{
    public class GetMemberRequest : IRequest<GetMemberResponse>
    {
        public int MemberID { get; set; }
    }
}