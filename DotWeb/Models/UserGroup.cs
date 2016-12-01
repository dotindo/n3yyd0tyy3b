using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb
{
    public class UserGroup
    {
        [Key, MaxLength(128)]
        public string Id { get; set; }

        [Required, MaxLength(50)]
        public string GroupName { get; set; }

        public string Description { get; set; }

        public int? AppId { get; set; }

        public virtual App App { get; set; }
    }
}
