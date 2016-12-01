using System;

namespace DotWeb.Models
{
    public partial class ControlPlanDetail3
    {
        public int Id { get; set; }
        public Nullable<int> ControlPlanDetail1Id { get; set; }
        public Nullable<int> ToolId { get; set; }
        public Nullable<int> Qty { get; set; }
        public string RF { get; set; }
        public string Torque { get; set; }
        public string Volume { get; set; }
        public Nullable<bool> IsAssign { get; set; }
    }
}
