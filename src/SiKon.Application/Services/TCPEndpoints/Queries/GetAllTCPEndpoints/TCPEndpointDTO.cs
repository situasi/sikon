namespace SiKon.Application.Services.TCPEndpoints.Queries.GetAllTCPEndpoints
{
    public class TCPEndpointDTO
    {
        public string FriendlyName { get; set; }
        public string TargetAddress { get; set; }
        public int PortNumber { get; set; }

        public string TargetAddressWithPortNumber
        {
            get
            {
                return $"{this.TargetAddress}:{this.PortNumber}";
            }
        }
    }
}