using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.GuidEntities;

namespace Domain.Entities.Images
{
    public class Image : GuidEntityBase
    {
        public string UrlTemplate { get; set; }

        public ICollection<ImageVersion> Versions { get; set; } = new List<ImageVersion>();

        public Uri GetImageUri(int size)
        {
            var version = Versions.FirstOrDefault(x => x.Size == size);
            if (version == null) return null;

            var result = String.Format(UrlTemplate, version.TemplateValue);
            return new Uri(result);
        }

        public Image AddVersion(int size, string templateValue)
        {
            var imageVersion = new ImageVersion
            {
                Size = size,
                TemplateValue = templateValue ?? String.Empty,
                ImageId = Id
            };

            Versions.Add(imageVersion);
            return this;
        }
    }
}