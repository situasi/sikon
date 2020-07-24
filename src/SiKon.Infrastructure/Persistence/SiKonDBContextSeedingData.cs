using System.Linq;
using System.Threading.Tasks;
using SiKon.Domain.Entities;

namespace SiKon.Infrastructure.Persistence
{
    public static class SiKonDBContextSeedingData
    {
        public static async Task SeedSampleDataAsync(SiKonDBContext context)
        {
            Member member = new Member
            {
                MemberUsername = "fuady@live.com",
                FullName = "Fuady Rosma Hidayat"
            };

            if (!context.Members.Any())
            {
                context.Members.Add(member);

                await context.SaveChangesAsync();
            }

            if (!context.TCPEndpoints.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    context.TCPEndpoints.Add(new TCPEndpoint
                    {
                        Member = member,
                        FriendlyName = $"TCP Endpoint No {i}",
                        TargetAddress = $"127.0.0.{i}",
                        PortNumber = 8080 + i,
                        CommandString = $"Command String {i}",
                        SuccessString = $"Success String {i}",
                        ErrorString = $"Error String {i}",
                        CheckIntervalInMinutes = 5,
                        RequestTimeOutInSeconds = 30
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}