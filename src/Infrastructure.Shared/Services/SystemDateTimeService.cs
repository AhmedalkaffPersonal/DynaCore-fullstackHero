using DynaCore.Application.Interfaces.Services;
using System;

namespace DynaCore.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}