using System;

namespace DddCore.Domain.ValueObjects.DateTimeRanges
{
    public class DateTimeRangeFactory : IDateTimeRangeFactory
    {
        public DateTimeRange Create(DateTimeRangeType periodType, DateTime? current = null)
        {
            return Create(periodType, 0, current);
        }

        public DateTimeRange Create(DateTimeRangeType periodType, int periodsAgo, DateTime? current = null)
        {
            DateTime today = current?.Date ?? DateTime.Today;

            switch (periodType)
            {
                case DateTimeRangeType.Day:
                    return CreateDayPeriod(today.AddDays(-periodsAgo));

                case DateTimeRangeType.Week:
                    return CreateWeekPeriod(today.AddDays(-7 * periodsAgo));

                case DateTimeRangeType.Month:
                    return CreateMonthPeriod(today.AddMonths(-periodsAgo));

                case DateTimeRangeType.Year:
                    return CreateYearPeriod(today.AddYears(-periodsAgo));

                case DateTimeRangeType.AllTime:
                    return CreateAllTimePeriod();

                default:
                    throw new ArgumentException(nameof(periodType));
            }
        }

        #region Private Methods

        DateTimeRange CreateDayPeriod(DateTime current)
        {
            return new DateTimeRange(current, current);
        }

        DateTimeRange CreateWeekPeriod(DateTime current)
        {
            DateTimeRange period = null;

            if (current.DayOfWeek == DayOfWeek.Monday)
            {
                period = new DateTimeRange(current, current.AddDays(6));
            }

            if (current.DayOfWeek == DayOfWeek.Tuesday)
            {
                period = new DateTimeRange(current.AddDays(-1), current.AddDays(5));
            }

            if (current.DayOfWeek == DayOfWeek.Wednesday)
            {
                period = new DateTimeRange(current.AddDays(-2), current.AddDays(4));
            }

            if (current.DayOfWeek == DayOfWeek.Thursday)
            {
                period = new DateTimeRange(current.AddDays(-3), current.AddDays(3));
            }

            if (current.DayOfWeek == DayOfWeek.Friday)
            {
                period = new DateTimeRange(current.AddDays(-4), current.AddDays(2));
            }

            if (current.DayOfWeek == DayOfWeek.Saturday)
            {
                period = new DateTimeRange(current.AddDays(-5), current.AddDays(1));
            }

            if (current.DayOfWeek == DayOfWeek.Sunday)
            {
                period = new DateTimeRange(current.AddDays(-6), current);
            }

            return period;
        }

        DateTimeRange CreateMonthPeriod(DateTime current)
        {
            return new DateTimeRange(new DateTime(current.Year, current.Month, 1), new DateTime(current.Year, current.Month, DateTime.DaysInMonth(current.Year, current.Month)));
        }

        DateTimeRange CreateYearPeriod(DateTime current)
        {
            return new DateTimeRange(new DateTime(current.Year, 1, 1), new DateTime(current.Year, 12, 31));
        }

        DateTimeRange CreateAllTimePeriod()
        {
            return new DateTimeRange(new DateTime(DateTime.Now.AddYears(-10).Year, 1, 1), new DateTime(DateTime.Now.Year, 12, 31));
        }

        #endregion
    }
}
