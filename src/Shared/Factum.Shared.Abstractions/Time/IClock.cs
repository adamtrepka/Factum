using System;

namespace Factum.Shared.Abstractions.Time;

public interface IClock
{
    DateTime CurrentDate();
}