using System.Linq;
using System.Threading.Tasks;
using SiKon.Domain.Entities;

namespace SiKon.Infrastructure.Persistence
{
    public static class SiKonDBContextSeedingData
    {
        public static async Task SeedSampleDataAsync(SiKonDBContext context)
        {
            Member member1 = new Member
            {
                Username = "fuady@live.com",
                FullName = "Fuady Rosma Hidayat"
            };

            Member member2 = new Member
            {
                Username = "raka.satria@hotmail.com",
                FullName = "Raka Satria"
            };

            if (!context.Members.Any())
            {
                context.Members.Add(member1);
                context.Members.Add(member2);

                await context.SaveChangesAsync();
            }

            if (!context.TCPEndpoints.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    TCPEndpoint tcpEndpoint = new TCPEndpoint
                    {
                        FriendlyName = $"TCP Endpoint No {i}",
                        TargetAddress = $"127.0.0.{i}",
                        PortNumber = 8080 + i,
                        CommandString = $"Command String {i}",
                        SuccessString = $"Success String {i}",
                        ErrorString = $"Error String {i}",
                        CheckIntervalInMinutes = 5,
                        RequestTimeOutInSeconds = 30
                    };

                    if (i % 2 == 0)
                    {
                        tcpEndpoint.Member = member1;
                    }
                    else
                    {
                        tcpEndpoint.Member = member2;
                    }

                    context.TCPEndpoints.Add(tcpEndpoint);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}