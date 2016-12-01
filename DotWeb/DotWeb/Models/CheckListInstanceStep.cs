namespace DotWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CheckListInstanceStep")]
    public partial class CheckListInstanceStep
    {
        public int Id { get; set; }

        public int? CheckListInstanceInfoId { get; set; }

        public int? SeqNo { get; set; }

        [StringLength(200)]
        public string StepName { get; set; }

        public int? UserGroupId { get; set; }

        public byte? Status { get; set; }

        [StringLength(500)]
        public string UrlLink { get; set; }

        [StringLength(100)]
        public string StoreProcedureName { get; set; }

        [StringLength(500)]
        public string EmailNotification { get; set; }

        public DateTime? StepProcessStart { get; set; }

        public DateTime? StepProcessEnd { get; set; }

        [StringLength(100)]
        public string Predecessor { get; set; }

        public int? UserId { get; set; }

        public virtual CheckListInstanceInfo CheckListInstanceInfo { get; set; }

        //public virtual UserGroup UserGroup { get; set; }

        //public virtual User User { get; set; }
    }
}
