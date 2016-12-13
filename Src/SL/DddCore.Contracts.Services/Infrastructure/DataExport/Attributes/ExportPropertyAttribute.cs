using System;

namespace DddCore.Contracts.Services.Infrastructure.DataExport.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportPropertyAttribute : Attribute
    {
        public string HeaderName { get; }

        public ExportPropertyAttribute(string headerName)
        {
            HeaderName = headerName;
        }
    }
}