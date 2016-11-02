using System;
using System.IO;
using Contracts.Services.Infrastructure.Preview;
using Contracts.Services.Infrastructure.Preview.Models;
using ImageMagick;

namespace Services.Infrastructure.Preview
{
    public class PreviewService : IPreviewService
    {
        public PreviewSummary GeneratePreview(Stream file, int maxWidth, int maxHeight)
        {
            var settings = new MagickReadSettings
            {
                Density = new Density(300, 300)
            };

            file.Position = 0;

            using (var image = new MagickImage())
            {
                image.Read(file, settings);
                image.Format = MagickFormat.Png;

                decimal resultRatio = maxHeight/(decimal) maxWidth;
                decimal currentRatio = image.Height/(decimal) image.Width;

                if (image.Width > maxWidth)
                {
                    String newGeomStr = maxWidth + "x" + (maxWidth/image.Width)*image.Height;

                    var intermediateGeo = new MagickGeometry(newGeomStr);


                    image.Resize(intermediateGeo);
                }


                //image.Thumbnail(maxWidth, maxHeight);

                var stream = new MemoryStream();
                image.Write(stream);

                stream.Position = 0;

                var result = new PreviewSummary
                {
                    File = stream,
                    Height = maxHeight,
                    Width = maxWidth
                };
                stream.Position = 0;

                return result;
            }
        }
    }
}
