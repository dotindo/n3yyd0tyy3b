using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Serializable]
    [Table("Variants")]
    public partial class Variant
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        [Column("Variant")]
        [Required]
        [StringLength(150)]
        public string Variant1 { get; set; }

        public virtual Model Model { get; set; }
    }
}
