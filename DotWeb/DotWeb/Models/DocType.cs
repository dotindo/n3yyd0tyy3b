using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    public partial class DocType
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocType()
        {
            this.Attachments = new HashSet<Attachment>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
