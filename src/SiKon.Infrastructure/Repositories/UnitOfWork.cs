using SiKon.Application.Interfaces;

namespace SiKon.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IMemberRepository memberRepository,
            ITCPEndpointRepository tcpEndpointRepository)
        {
            Members = memberRepository;
            TCPEndpoints = tcpEndpointRepository;
        }

        public IMemberRepository Members { get; }
        public ITCPEndpointRepository TCPEndpoints { get; }
    }
}