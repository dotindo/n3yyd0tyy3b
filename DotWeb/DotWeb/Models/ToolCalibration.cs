using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    [Table("ToolCalibrations")]
    public partial class ToolCalibration
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> CalDate { get; set; }
        public string CalNumber { get; set; }
        public Nullable<int> ToolSetupId { get; set; }
        public Nullable<int> ToolSetupInv { get; set; }
        public Nullable<decimal> SetNM { get; set; }
        public Nullable<decimal> MinNM { get; set; }
        public Nullable<decimal> MaxNM { get; set; }
        public Nullable<decimal> Verification1 { get; set; }
        public Nullable<decimal> Verification2 { get; set; }
        public Nullable<decimal> Verification3 { get; set; }
        public Nullable<decimal> Verification4 { get; set; }
        public Nullable<decimal> Verification5 { get; set; }
        public Nullable<int> ResultId { get; set; }
        public Nullable<System.DateTime> NextCalibrationDate { get; set; }
        public string Remarks { get; set; }

       // public virtual ToolCalibrationResult ToolCalibrationResult { get; set; }
       // public virtual ToolSetup ToolSetup { get; set; }
    }
}   
