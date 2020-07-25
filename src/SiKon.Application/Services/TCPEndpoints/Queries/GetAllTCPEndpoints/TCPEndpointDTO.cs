namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public class TCPEndpointDTO
    {
        public int MemberID { get; set; }
        public string FriendlyName { get; set; }
        public string TargetAddress { get; set; }
        public int PortNumber { get; set; }

        public string MemberUsername { get; set; }
        public string MemberFullName { get; set; }

        public string TargetAddressWithPortNumber
        {
            get
            {
                return $"{this.TargetAddress}:{this.PortNumber}";
            }
        }
    }
}