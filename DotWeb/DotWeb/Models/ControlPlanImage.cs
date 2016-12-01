using System;

namespace DotWeb.Models
{
    public partial class ControlPlanImage
    {
        public int Id { get; set; }
        public Nullable<int> ControlPlanProcessId { get; set; }
        public byte[] Image { get; set; }

        public virtual ControlPlanProcess ControlPlanProcess { get; set; }
    }
}
