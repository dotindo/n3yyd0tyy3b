using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("CGISSynchronizeds")]
    public partial class CGISSynchronized
    {
        public int Id { get; set; }
        public string PackingMonth { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public string ProcessBy { get; set; }

        public virtual Type Type { get; set; }
        public virtual Model Model { get; set; }
    }
}
