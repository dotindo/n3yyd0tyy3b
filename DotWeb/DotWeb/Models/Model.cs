using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    [Serializable]
    public partial class Model
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            Variants = new HashSet<Variant>();
        }

        public int Id { get; set; }

        public int? TypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string Baumuster { get; set; }

        [StringLength(50)]
        public string ModelName { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Variant> Variants { get; set; }

        public virtual Type Type { get; set; }
    }
}
