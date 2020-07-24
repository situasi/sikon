using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SiKon.Domain.Common;

namespace SiKon.Domain.Entities
{
    public class Member : AuditableEntity
    {
        [Key]
        public string MemberUsername { get; set; }

        public string FullName { get; set; }

        public IList<TCPEndpoint> TCPEndpoints { get; set; }

        public Member()
        {
            this.TCPEndpoints = new List<TCPEndpoint>();
        }
    }
}