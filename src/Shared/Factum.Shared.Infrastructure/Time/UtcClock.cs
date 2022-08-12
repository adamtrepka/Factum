using Factum.Shared.Abstractions.Time;
using System;

namespace Factum.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}