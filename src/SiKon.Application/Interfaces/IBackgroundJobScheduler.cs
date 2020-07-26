using System.Threading.Tasks;

namespace SiKon.Application.Interfaces
{
    public interface IBackgroundJobScheduler
    {
        Task RegisterToScheduler(string message);
    }
}