using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    [Table("CPHeader")]
    public partial class CPHeader
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPHeader()
        {
            this.CPDetails = new HashSet<CPDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> PackingMonthId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> VariantId { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPDetail> CPDetails { get; set; }
    }
}
