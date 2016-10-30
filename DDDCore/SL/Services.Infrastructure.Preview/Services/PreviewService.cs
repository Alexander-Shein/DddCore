using System;
using System.Collections.Generic;
using System.IO;
using Contracts.Crosscutting.Configuration;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.Files.Services;
using Contracts.Services.Infrastructure.Preview.Models;
using Contracts.Services.Infrastructure.Preview.Services;
using ImageMagick;

namespace Services.Infrastructure.Preview.Services
{
    public class PreviewService : IPreviewService
    {
        private static class Consts
        {
            public const string PreviewFileExtension = ".png";

            public const string PreviewMaxWidth = "ImagePreview_MaxWidth";
            public const string PreviewMaxHeight = "ImagePreview_MaxHeight";

            public const string ThumbnailMaxWidth = "ImageThumbnail_MaxWidth";
            public const string ThumbnailMaxHeight = "ImageThumbnail_MaxHeight";

            public const string PreviewLocationType = "Previews";
        }

        public IConfig Config { get; set; }
        public IFileService FileService { get; set; }

        public PreviewSummaries GeneratePreviews(FileSummary file, PreviewType size)
        {
            return new PreviewSummaries(new List<PreviewSummary> { GeneratePreview(file, size) });
        }

        public PreviewSummary GeneratePreview(FileSummary fileSummary, PreviewType size)
        {
            var file = FileService.Read(fileSummary);

            var settings = new MagickReadSettings
            {
                Density = new Density(300, 300)
            };

            file.Position = 0;

            bool isPreview = size == PreviewType.Preview;

            int width = isPreview
                ? Config.Get<int>(Consts.PreviewMaxWidth)
                : Config.Get<int>(Consts.ThumbnailMaxWidth);

            int height = isPreview
                ? Config.Get<int>(Consts.PreviewMaxHeight)
                : Config.Get<int>(Consts.ThumbnailMaxHeight);

            using (var image = new MagickImage())
            {
                image.Read(file, settings);
                image.Format = MagickFormat.Png;

                decimal resultRatio = height / (decimal)width;
                decimal currentRatio = image.Height / (decimal)image.Width;

                if(image.Width > width)
                {
                    String newGeomStr = width + "x" + (width / image.Width) * image.Height;

                    var intermediateGeo = new MagickGeometry(newGeomStr);


                    image.Resize(intermediateGeo);
                }


                //image.Thumbnail(maxWidth, maxHeight);

                var stream = new MemoryStream();
                image.Write(stream);

                stream.Position = 0;

                var fileSummaryResult = FileService.Upload(new FileDetails
                {
                    LocationType = Consts.PreviewLocationType,
                    File = stream,
                    OriginalFileName = Consts.PreviewFileExtension
                });

                var result = new PreviewSummary
                {
                    FileSummary = fileSummaryResult,
                    Size = size
                };
                stream.Position = 0;

                return result;
            }
        }
    }
}
