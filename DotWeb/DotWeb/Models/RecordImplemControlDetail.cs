using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    public partial class RecordImplemControlDetail
    {
        public int Id { get; set; }
        public Nullable<int> RICId { get; set; }
        public Nullable<int> OldNo { get; set; }
        public string OldPartNo { get; set; }
        public string OldPartDescription { get; set; }
        public Nullable<decimal> OldQty { get; set; }
        public string OldESD { get; set; }
        public string NewPartNo { get; set; }
        public string NewPartDescription { get; set; }
        public Nullable<decimal> NewQty { get; set; }
        public string NewESD { get; set; }
        public string PackID { get; set; }
        public string NewIndex { get; set; }
        public string DialogAddress { get; set; }
        public string CGISNumber { get; set; }
        public string Remark { get; set; }
        public string NewPartNumber { get; set; }
        public string NewDescription { get; set; }
        public string PartNumberGab { get; set; }
        public string OldCode { get; set; }
        public string NewCode { get; set; }

        public virtual RecordImplemControl RecordImplemControl { get; set; }
    }
}
