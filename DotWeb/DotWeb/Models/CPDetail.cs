using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    public partial class CPDetail
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPDetail()
        {
            this.CPConsumptionMaterials = new HashSet<CPConsumptionMaterial>();
            this.CPToolLists = new HashSet<CPToolList>();
        }

        public int Id { get; set; }
        public Nullable<int> CPHeaderId { get; set; }
        public Nullable<int> AssemblySectionId { get; set; }
        public Nullable<int> StationId { get; set; }
        public Nullable<int> ProcessNo { get; set; }
        public string ProcessName { get; set; }
        public string CgisNr { get; set; }
        public string OriCgisNr { get; set; }
        public string TorqueInfo { get; set; }
        public string RF { get; set; }
        public string DialogAddress { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public Nullable<int> Qty { get; set; }
        public string RK { get; set; }
        public string Class { get; set; }
        public string TrollyNo { get; set; }
        public string VDok { get; set; }
        public string SecondHand { get; set; }
        public string DS { get; set; }
        public string DRT { get; set; }
        public string CS2 { get; set; }
        public string CS3 { get; set; }
        public string SaCode { get; set; }
        public string TextInJobCard { get; set; }
        public bool IsPrintToJobCard { get; set; }
        public Nullable<DateTime> ValidFrom { get; set; }
        public Nullable<DateTime> ValidTo { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual AssemblySection AssemblySection { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPConsumptionMaterial> CPConsumptionMaterials { get; set; }
        public virtual CPHeader CPHeader { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPToolList> CPToolLists { get; set; }
    }
}
