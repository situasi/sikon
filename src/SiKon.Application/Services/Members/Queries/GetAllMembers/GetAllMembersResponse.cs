using System.Collections.Generic;

namespace SiKon.Application.Services.Members.Queries.GetAllMembers
{
    public class GetAllMembersResponse
    {
        public IList<MemberDTO> Members { get; set; }

        public GetAllMembersResponse()
        {
            this.Members = new List<MemberDTO>();
        }
    }
}