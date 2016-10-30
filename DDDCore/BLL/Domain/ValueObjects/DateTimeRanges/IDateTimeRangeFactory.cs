using System;

namespace Domain.ValueObjects.DateTimeRanges
{
    public interface IDateTimeRangeFactory
    {
        DateTimeRange Create(DateTimeRangeType dateTimeRangeType, DateTime? current = null);
        DateTimeRange Create(DateTimeRangeType dateTimeRangeType, int periodsAgo, DateTime? current = null);
    }
}