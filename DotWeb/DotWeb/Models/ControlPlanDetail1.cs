using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    public partial class ControlPlanDetail1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlPlanDetail1()
        {
            this.ControlPlanDetail4 = new HashSet<ControlPlanDetail4>();
        }

        public int id { get; set; }
        public Nullable<int> ControlPlanId { get; set; }
        public Nullable<int> SeqNo { get; set; }
        public Nullable<int> ProcessNo { get; set; }
        public string ProcessName { get; set; }
        public Nullable<int> AssemblySectionID { get; set; }
        public Nullable<int> StationId { get; set; }
        public string AssemblyArea { get; set; }
        public string AssemblyAreaName { get; set; }
        public string CgisNr { get; set; }
        public string OriCgisNr { get; set; }
        public string Rf { get; set; }
        public string DialogAddress { get; set; }
        public Nullable<int> PartNumber { get; set; }
        public Nullable<int> PartDescription { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string Rk { get; set; }
        public Nullable<int> TrolleyNo { get; set; }
        public string StampNr { get; set; }
        public string PartNumber1 { get; set; }
        public string PartDescription1 { get; set; }
        public string Deviasi { get; set; }
        public string IDProcess { get; set; }
        public string TextJC { get; set; }
        public Nullable<bool> JCStamp { get; set; }
        public Nullable<bool> Invalid { get; set; }
        public string IOCriteria { get; set; }

        public virtual ControlPlan ControlPlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPlanDetail4> ControlPlanDetail4 { get; set; }
    }
}
