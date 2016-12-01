using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    [Table("Tools")]
    public partial class Tool
    {
       
        public Tool()
        {
          //  this.ToolCalibrations = new HashSet<ToolCalibration>();
            //this.ToolInventories = new HashSet<ToolInventory>();
            //this.ToolVerifications = new HashSet<ToolVerification>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public Nullable<int> ToolSupplierId { get; set; }
        public Nullable<int> ToolBrandId { get; set; }
        public string Specification { get; set; }
        public string OrderNo { get; set; }
        public Nullable<bool> Calibrated { get; set; }
        public Nullable<decimal> MinNM { get; set; }
        public Nullable<decimal> MaxNM { get; set; }

       // public virtual ICollection<ToolCalibration> ToolCalibrations { get; set; }
       
        //public virtual ICollection<ToolInventory> ToolInventories { get; set; }
        //public virtual ICollection<ToolVerification> ToolVerifications { get; set; }
        public virtual ToolSupplier ToolSupplier { get; set; }
    }
}
