using Microsoft.AspNetCore.Http;
using SiKon.Application.Interfaces;

namespace SiKon.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Username { get; }
        public bool IsAuthenticated { get; }

        //public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        //{
        //    Username = "fuady@live.com";
        //    IsAuthenticated = Username != null;
        //}

        public CurrentUserService()
        {
            Username = "fuady@live.com";
            IsAuthenticated = Username != null;
        }
    }
}