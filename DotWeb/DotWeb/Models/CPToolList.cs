using System;

namespace DotWeb.Models
{
    public partial class CPToolList
    {
        public int Id { get; set; }
        public Nullable<int> CPDetailId { get; set; }
        public Nullable<int> ToolListId { get; set; }
        public Nullable<int> UsageStationId { get; set; }
        public string UsageModel { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }

        public virtual CPDetail CPDetail { get; set; }
    }
}
