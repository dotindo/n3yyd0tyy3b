namespace DotWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckListTemplateStep")]
    public partial class CheckListTemplateStep
    {
        public int Id { get; set; }

        public int? CheckListTemplateInfoId { get; set; }

        [StringLength(500)]
        public string StepName { get; set; }

        public int? UserId { get; set; }

        public int? UserGroupId { get; set; }

        public int? ChecklistTypeId { get; set; }

        [StringLength(500)]
        public string UrlLink { get; set; }

        [StringLength(100)]
        public string StoreProcedureName { get; set; }

        public bool? IsSendEmailNotification { get; set; }

        [StringLength(500)]
        public string EmailNotification { get; set; }

        public int? SeqNo { get; set; }

        public byte? RowStatus { get; set; }

        [StringLength(100)]
        public string Predecessor { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public virtual CheckListTemplateInfo CheckListTemplateInfo { get; set; }

        public virtual ChecklistType ChecklistType { get; set; }

        //public virtual UserGroup UserGroup { get; set; }

        //public virtual User User { get; set; }
    }
}
