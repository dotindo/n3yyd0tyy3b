namespace DotWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckListInstanceInfo")]
    public partial class CheckListInstanceInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CheckListInstanceInfo()
        {
            CheckListInstanceSteps = new HashSet<CheckListInstanceStep>();
        }

        public int Id { get; set; }

        public int? CheckListTemplateInfoId { get; set; }

        [StringLength(6)]
        public string PackingMonth { get; set; }

        [StringLength(20)]
        public string Model { get; set; }

        [StringLength(20)]
        public string Variant { get; set; }

        [StringLength(20)]
        public string RunningNumber { get; set; }

        [StringLength(100)]
        public string InstanceName { get; set; }

        [StringLength(100)]
        public string InstanceDocument { get; set; }

        [StringLength(200)]
        public string LastActivity { get; set; }

        public decimal? Progress { get; set; }

        public byte RowStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public int CheckListGroupId { get; set; }

        public virtual CheckListGroup CheckListGroup { get; set; }

        public virtual CheckListTemplateInfo CheckListTemplateInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckListInstanceStep> CheckListInstanceSteps { get; set; }
    }
}
