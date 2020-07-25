using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Services.Members.Queries.GetAllMembers
{
    public static class GetAllMembersQuery
    {
        public static async Task<GetAllMembersResponse> HandleWithEFCore(ISiKonDBContext database)
        {
            var members = await database.Members.ToListAsync();

            GetAllMembersResponse response = new GetAllMembersResponse();

            foreach (var member in members)
            {
                MemberDTO memberDTO = new MemberDTO
                {
                    MemberID = member.MemberID,
                    Username= member.Username,
                    FullName= member.FullName
                };

                response.Members.Add(memberDTO);
            }

            return response;
        }

        public static async Task<GetAllMembersResponse> HandleWithDapper(IUnitOfWork _unitOfWork)
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