using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SiKon.Domain.Common;

namespace SiKon.Domain.Entities
{
    public class Member : AuditableEntity
    {
        [Key]
        public int MemberID { get; set; }

        public string Username { get; set; }
        public string FullName { get; set; }

        public IList<TCPEndpoint> TCPEndpoints { get; set; }

        public Member()
        {
            this.TCPEndpoints = new List<TCPEndpoint>();
        }
    }
}