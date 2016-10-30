using System.ComponentModel;

namespace Contracts.Domain.Entities.Model
{
    public enum CrudState
    {
        [Description("Unchanged")]
        Unchanged,

        [Description("Added")]
        Added,

        [Description("Modified")]
        Modified,

        [Description("Deleted")]
        Deleted
    }
}