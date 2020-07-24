using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiKon.Domain.Common;

namespace SiKon.Domain.Entities
{
    public class TCPEndpoint : AuditableEntity
    {
        [Key]
        public int TCPEndpointID { get; set; }

        [ForeignKey(nameof(MemberUsername))]
        public string MemberUsername { get; set; }
        public Member Member { get; set; }

        public string FriendlyName { get; set; }
        public string TargetAddress { get; set; }
        public int PortNumber { get; set; }
        public string CommandString { get; set; }
        public string SuccessString { get; set; }
        public string ErrorString { get; set; }
        public int CheckIntervalInMinutes { get; set; }
        public int RequestTimeOutInSeconds { get; set; }
    }
}