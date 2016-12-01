using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    [Table("ToolSetup")]
    public partial class ToolSetup
    {

        public ToolSetup()
        {
            ToolVerifications = new HashSet<ToolVerification>();
            Tools = new HashSet<Tool>();
            ToolCalibrations = new HashSet<ToolCalibration>();
        }
        public int Id { get; set; }
        public string Number { get; set; }
        public string Decription { get; set; }
        public int SupplierId { get; set; }
        public int BrandId { get; set; }
        public string Specification { get; set; }
        public string OrderNo { get; set; }
        public int CalibrateId { get; set; }
        public int SetNM { get; set; }
        public int MinNM { get; set; }
        public int MaxNM { get; set; }
        public virtual ICollection<ToolVerification> ToolVerifications { get; set; }
        public virtual ICollection<Tool> Tools { get; set; }
        public virtual ICollection<ToolCalibration> ToolCalibrations { get; set; }
    }
}
