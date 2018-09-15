<Query Kind="Program" />

#region Interface

interface IDateTimeService
{
    DateTimeOffset GetUtcNow();
}

class RealDateTimeService
    : IDateTimeService
{
    DateTimeOffset IDateTimeService.GetUtcNow() => DateTime.UtcNow;
}

class FakeDateTimeService
    : IDateTimeService
{
    private readonly DateTimeOffset _now;
    
    public FakeDateTimeService(DateTimeOffset now)
    {
        _now = now;
    }
    
    DateTimeOffset IDateTimeService.GetUtcNow() => _now;
}

void WhatTimeIsIt(IDateTimeService svc) => svc.GetUtcNow().Dump();

#endregion

#region Delegate

void WhatTimeIsIt(Func<DateTimeOffset> svc) => svc().Dump();

#endregion

void Main()
{
    var fakeTime = DateTimeOffset.Parse("2017-10-04T19:00:00-04:00").ToUniversalTime();
    
    WhatTimeIsIt(new RealDateTimeService());
    WhatTimeIsIt(new FakeDateTimeService(fakeTime));
    
    WhatTimeIsIt(() => DateTime.UtcNow);
    WhatTimeIsIt(() => fakeTime);
}