using System.ComponentModel;

namespace Domain.ValueObjects.DateTimeRanges
{
    public enum DateTimeRangeType
    {
        [Description("day")]
        Day,

        [Description("week")]
        Week,

        [Description("month")]
        Month,

        [Description("year")]
        Year,

        [Description("alltime")]
        AllTime
    }
}