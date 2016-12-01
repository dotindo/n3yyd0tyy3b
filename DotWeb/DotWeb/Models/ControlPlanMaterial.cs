using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public partial class ControlPlanMaterial
    {
        public int Id { get; set; }
        public Nullable<int> ControlPlanProcessId { get; set; }
        public Nullable<decimal> RF { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public Nullable<int> Qty { get; set; }
        public string QtyUnit { get; set; }

        public virtual ControlPlanProcess ControlPlanProcess { get; set; }
    }
}
