using System;
using SiKon.Application.Interfaces;

namespace SiKon.Infrastructure.Services
{
    public class DateTimeOffsetService : IDateTimeOffsetService
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}