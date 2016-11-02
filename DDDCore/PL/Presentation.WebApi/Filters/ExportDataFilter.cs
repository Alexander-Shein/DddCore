using System;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Contracts.Services.Infrastructure.DataExport;
using Crosscutting.Infrastructure;
using Services.Infrastructure.DataExport;

namespace Presentation.WebApi.Filters
{
    public class ExportDataFilter : ActionFilterAttribute
    {
        private readonly ExportType _exportType;
        private readonly IDataExporter _exporter;
        private readonly IDataExporterFactory _exporterFactory = new DataExporterFactory();
        private readonly string _exportFileName;

        private string _acceptHeader;

        public ExportDataFilter(ExportType exportType, string exportFileName)
        {
            _exportType = exportType;
            _exporter = _exporterFactory.CreateDataExporter(exportType);
            SetAcceptHeaderAndFileExtension();

            _exportFileName = exportFileName;

            if (string.IsNullOrEmpty(_acceptHeader) || string.IsNullOrEmpty(_exportFileName))
                throw new ArgumentException();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var acceptHeader = actionContext.Request.Headers.Accept?.FirstOrDefault();
            //
            //if (acceptHeader == null || acceptHeader.MediaType != _acceptHeader)
            //    return;
            //
            //var controller = actionContext.ControllerContext.Controller as ApiControllerBase;
            //
            //if (controller == null)
            //    return;
            //
            //controller.IsExport = true;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as ApiControllerBase;
            //
            //if (controller == null || !controller.IsExport)
            //    return;
            //
            //var dictionary = controller.ValidationErrorsDictionary;
            //
            //if (dictionary.Any())
            //    return;
            //
            //var actionResult = actionExecutedContext.Response?.Content as ObjectContent;
            //
            //var objects = actionResult?.Value as IEnumerable<object>;
            //
            //if (objects == null)
            //    return;
            //
            //var response = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new StreamContent(_exporter.Export(objects.ToArray()))
            //};
            //
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue(_acceptHeader);
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = _exportFileName
            //};
            //
            //actionExecutedContext.Response = response;
        }

        private void SetAcceptHeaderAndFileExtension()
        {
            var match = Regex.Match(_exportType.GetDescription(),
                @"AcceptHeader=(?<accept>[\w+\/]+)", RegexOptions.Singleline);

            _acceptHeader = match.Groups["accept"]?.ToString();
        }
    }
}