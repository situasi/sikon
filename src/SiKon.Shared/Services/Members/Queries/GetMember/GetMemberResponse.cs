using AutoMapper;
using SiKon.Domain.Entities;
using SiKon.Shared.Mappings;

namespace SiKon.Shared.Services.Members.Queries.GetMember
{
    public class GetMemberResponse : IMapFrom<Member>
    {
        public int MemberID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }

        // Contoh property tambahan yang gak tersedia di kolom di database
        public int PanjangNama { get; set; }

        public void Mapping(Profile profile)
        {
            // Kita harus mendefinisikan agar si AutoMapper bisa mengisi property tambahan tersebut
            // dengan nilai yang benar
            // tanpa mengubah RequestHandler
            profile.CreateMap<Member, GetMemberResponse>()
                .ForMember(d => d.PanjangNama, opt => opt.MapFrom(s => s.FullName.Length));
        }
    }
}