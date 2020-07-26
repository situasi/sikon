using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiKon.Application.Interfaces;
using SiKon.Shared.Services.TCPEndpoint.Commands.CreateTCPEndpoint;

namespace SiKon.Application.Services.TCPEndpoints.Commands.CreateTCPEndpoint
{
    public class CreateTCPEndpointCommandHandler : IRequestHandler<CreateTCPEndpointRequest, CreateTCPEndpointResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IBackgroundJobScheduler _scheduler;

        public CreateTCPEndpointCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser, IBackgroundJobScheduler scheduler)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _scheduler = scheduler;
        }

        public async Task<CreateTCPEndpointResponse> Handle(CreateTCPEndpointRequest request, CancellationToken cancellationToken)
        {
            await _scheduler.RegisterToScheduler("Bikin TCP Endpoint nih");

            CreateTCPEndpointResponse response = new CreateTCPEndpointResponse();

            return response;
        }
    }
}