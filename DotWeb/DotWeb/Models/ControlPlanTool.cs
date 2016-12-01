using System;
using System.Collections.Generic;

namespace DotWeb.Models
{
    public partial class ControlPlanTool
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlPlanTool()
        {
            this.ToolAssignments = new HashSet<ToolAssignment>();
        }

        public int Id { get; set; }
        public Nullable<int> ControlPlanProcessId { get; set; }
        public Nullable<decimal> RF { get; set; }
        public Nullable<int> ToolId { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Torque { get; set; }
        public string Volume { get; set; }

        public virtual ControlPlanProcess ControlPlanProcess { get; set; }
        public virtual Tool Tool { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolAssignment> ToolAssignments { get; set; }
    }
}
