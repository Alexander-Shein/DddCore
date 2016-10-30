using System;
using System.Linq;
using System.Reflection;
using Contracts.Services.Infrastructure.DataExport;
using Contracts.Services.Infrastructure.DataExport.Attributes;

namespace Services.Infrastructure.DataExport
{
    public abstract class DataExporterBase : IDataExporter
    {
        #region Pubic Methods

        public abstract ExportSummary Export(object[] objects);

        #endregion

        #region Protected Methods

        protected string[] GetHeaders(object @object)
        {
            var exportProperties =
                @object
                    .GetType()
                    .GetProperties()
                    .Where(x => Attribute.IsDefined((MemberInfo) x, typeof(ExportPropertyAttribute)));

            var propertyHeaders =
                exportProperties
                    .Select(x => ((ExportPropertyAttribute)x.GetCustomAttributes(typeof(ExportPropertyAttribute), false).First()).HeaderName)
                    .ToArray();

            return propertyHeaders;
        }

        protected string[] GetRow(object @object)
        {
            var exportProperties =
                @object
                    .GetType()
                    .GetProperties()
                    .Where(x => Attribute.IsDefined(x, typeof (ExportPropertyAttribute)));

            var propertyValues = exportProperties.Select(x => x.GetValue(@object, null)?.ToString().Trim()).ToArray();

            return propertyValues;
        }

        #endregion
    }
}