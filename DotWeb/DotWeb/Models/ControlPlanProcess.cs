using System;
using System.Collections.Generic;

namespace DotWeb.Models
{
    public partial class ControlPlanProcess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlPlanProcess()
        {
            this.ControlPlanImages = new HashSet<ControlPlanImage>();
            this.ControlPlanMaterials = new HashSet<ControlPlanMaterial>();
            this.ControlPlanTools = new HashSet<ControlPlanTool>();
        }

        public int Id { get; set; }
        public Nullable<int> ControlPlanId { get; set; }
        public Nullable<int> ProcessNo { get; set; }
        public string ProcessName { get; set; }
        public Nullable<int> StationId { get; set; }
        public Nullable<decimal> Rf { get; set; }
        public string CgisNo { get; set; }
        public string OriCgisNo { get; set; }
        public string DialogAddress { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string Rk { get; set; }
        public Nullable<int> TrolleyId { get; set; }
        public string Deviation { get; set; }
        public string TextJC { get; set; }
        public Nullable<bool> Invalid { get; set; }
        public string IOCriteria { get; set; }
        public string Class { get; set; }
        public Nullable<bool> VDoc { get; set; }
        public Nullable<bool> SecondHand { get; set; }
        public string FBS { get; set; }
        public Nullable<bool> DS { get; set; }
        public Nullable<bool> CS2 { get; set; }
        public Nullable<bool> CS3 { get; set; }
        public string DRT { get; set; }
        public Nullable<bool> JCStamp { get; set; }
        public Nullable<bool> JCBarcode { get; set; }
        public string StampNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPlanImage> ControlPlanImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPlanMaterial> ControlPlanMaterials { get; set; }
        public virtual ControlPlan ControlPlan { get; set; }
        public virtual Stations Station { get; set; }
        public virtual Trolley Trolley { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPlanTool> ControlPlanTools { get; set; }
    }
}
