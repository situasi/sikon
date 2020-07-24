namespace SiKon.Application.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        string Username { get; }
    }
}