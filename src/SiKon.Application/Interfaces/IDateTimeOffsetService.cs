using System;

namespace SiKon.Application.Interfaces
{
    public interface IDateTimeOffsetService
    {
        DateTimeOffset Now { get; }
    }
}