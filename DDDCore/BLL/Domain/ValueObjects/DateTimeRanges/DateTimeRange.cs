using System;

namespace Domain.ValueObjects.DateTimeRanges
{
    public class DateTimeRange : ValueObject<DateTimeRange>
    {
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException();

            Start = start;
            End = end;
        }

        public bool IsInDateRange(DateTime date)
        {
            return Start.Date <= date && date <= End.Date;
        }

        public bool IsInDateTimeRange(DateTime date)
        {
            return Start <= date && date <= End;
        }

        protected DateTimeRange() { }
    }
}