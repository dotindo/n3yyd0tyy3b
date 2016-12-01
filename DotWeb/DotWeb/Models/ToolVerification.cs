using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
     [Table("ToolVerifications")]
    public partial class ToolVerification
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> VerDate { get; set; }
        public string CalNumber { get; set; }
        public Nullable<int> ToolSetupId { get; set; }
        public Nullable<int> ToolSetupInv { get; set; }
        public Nullable<decimal> SetNM { get; set; }
        public Nullable<decimal> MinNM { get; set; }
        public Nullable<decimal> MaxNM { get; set; }
        public Nullable<decimal> Verification1 { get; set; }
        public Nullable<decimal> Verification2 { get; set; }
        public Nullable<decimal> Verification3 { get; set; }
        public Nullable<bool> ResultId { get; set; }
        public Nullable<System.DateTime> NextVerificationDate { get; set; }

        //public virtual Tool Tool { get; set; }
        //public virtual ToolInventory ToolInventory { get; set; }
    }
}
