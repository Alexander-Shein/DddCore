using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Contracts.Services.Infrastructure.Files.Models.Input;

namespace Presentation.WebApi
{
    public static class HttpContextExtensions
    {
        public static void GuardIsMimeMultipartContent(this HttpContent httpContent)
        {
            if (!httpContent.IsMimeMultipartContent())
            {
                throw new ArgumentException("Unsupported media type");
            }
        }

        public static async Task<FileDetails> ReadFileDetails(this HttpContent httpContent)
        {
            HttpContent fileContent = await ReadFileContent(httpContent);

            Stream stream = await fileContent.ReadAsStreamAsync();
            string fileName = CleanFileName(fileContent.Headers.ContentDisposition.FileName);

            var result = new FileDetails
            {
                File = stream,
                OriginalFileName = fileName
            };
            return result;
        }

        static async Task<HttpContent> ReadFileContent(HttpContent httpContent)
        {
            var multipart = await httpContent.ReadAsMultipartAsync();

            IEnumerable<HttpContent> parts = multipart.Contents;

            var fileContent = parts.First();

            return fileContent;
        }

        static string CleanFileName(string name)
        {
            return name.Trim('"');
        }
    }
}
