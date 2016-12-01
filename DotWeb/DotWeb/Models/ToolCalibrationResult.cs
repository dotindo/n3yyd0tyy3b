using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    [Table("ToolCalibrationResult")]

    public partial class ToolCalibrationResult
    {
        //public ToolCalibrationResult()
        //{
        //    this.ToolCalibrations = new HashSet<ToolCalibration>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<ToolCalibration> ToolCalibrations { get; set; }
    }
}
