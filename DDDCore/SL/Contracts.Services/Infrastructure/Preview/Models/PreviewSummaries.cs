using System.Collections.Generic;

namespace Contracts.Services.Infrastructure.Preview.Models
{
    public class PreviewSummaries : List<PreviewSummary>
    {
        public PreviewSummaries()
        {
        }

        public PreviewSummaries(IEnumerable<PreviewSummary> previews) : base(previews)
        {
        }
    }
}