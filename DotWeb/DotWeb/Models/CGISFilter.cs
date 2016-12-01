using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("CGISFilter")]
    public partial class CGISFilter
    {
        public int Id { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> AssemblySectionId { get; set; }
        public Nullable<int> StationId { get; set; }
        public string ProcessNoRangeStart { get; set; }
        public string ProcessNoRangeEnd { get; set; }
        public string ProcessException { get; set; }
        public Nullable<System.DateTime> LastSynchronizedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual AssemblySection AssemblySection { get; set; }
        public virtual Model Model { get; set; }
        public virtual Stations Station { get; set; }
    }
}

