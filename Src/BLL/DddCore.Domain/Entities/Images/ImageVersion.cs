using System;
using DddCore.Domain.Entities.GuidEntities;

namespace DddCore.Domain.Entities.Images
{
    public class ImageVersion : GuidEntityBase
    {
        public Guid ImageId { get; set; }
        public int Size { get; set; }
        public string TemplateValue { get; set; }
    }
}