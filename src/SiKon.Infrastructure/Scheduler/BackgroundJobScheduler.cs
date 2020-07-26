using System;
using System.Threading.Tasks;
using SiKon.Application.Interfaces;

namespace SiKon.Infrastructure.Scheduler
{
    public class BackgroundJobScheduler : IBackgroundJobScheduler
    {
        public Task RegisterToScheduler(string message)
        {
           Console.WriteLine($"BackgroundJob: {message}");

            return Task.CompletedTask;
        }
    }
}