namespace SiKon.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        ITCPEndpointRepository TCPEndpoints { get; }
    }
}