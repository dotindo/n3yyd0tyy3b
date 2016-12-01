using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    [Table("ToolAssignments")]
    public partial class ToolAssignment
    {
        public long Id { get; set; }
        public Nullable<int> ControlPlanToolId { get; set; }
        public Nullable<int> ToolInventoryId { get; set; }

      //  public virtual ToolInventory ToolInventory { get; set; }
    }
}
