using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public partial class ToolInventory
    {
        public int Id { get; set; }
        public Nullable<int> ToolSetupId { get; set; }
        public Nullable<int> InventoryNumber { get; set; }
        public Nullable<decimal> SetNm { get; set; }
        public Nullable<bool> Status { get; set; }

        //public virtual Tool Tool { get; set; }
    }
}
