using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DotWeb.Models
{
    [Table("CheckListGroup")]
    public partial class CheckListGroup
    {
        public CheckListGroup()
        {
            CheckListInstanceInfoes = new HashSet<CheckListInstanceInfo>();
            CheckListTemplateInfoes = new HashSet<CheckListTemplateInfo>();
        }

        public int Id { get; set; }

        [StringLength(5)]
        public string CheckListGroupCode { get; set; }

        [StringLength(50)]
        public string CheckListGroupName { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckListInstanceInfo> CheckListInstanceInfoes { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckListTemplateInfo> CheckListTemplateInfoes { get; set; }
    }
}
