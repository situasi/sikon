﻿using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SiKon.Application.Interfaces;

namespace SiKon.Application.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUser;

        public PerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUser)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 1000)
            {
                var requestName = typeof(TRequest).Name;
                string username = _currentUser.IsAuthenticated ? _currentUser.Username : "ANONYMOUS";

                _logger.LogWarning("SiKon Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Username}",
                    requestName, elapsedMilliseconds, username);
            }

            return response;
        }
    }
}