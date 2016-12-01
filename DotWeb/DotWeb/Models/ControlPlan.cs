using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    [Table("ControlPlan")]
    public partial class ControlPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlPlan()
        {
            this.ControlPlanProcesses = new HashSet<ControlPlanProcess>();
        }
    
        public int Id { get; set; }
        public string PackingMonth { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> VariantId { get; set; }
        public Nullable<int> CommnosFrom { get; set; }
        public Nullable<int> CommnosTo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPlanProcess> ControlPlanProcesses { get; set; }
        public virtual Model Model { get; set; }
    }
}
