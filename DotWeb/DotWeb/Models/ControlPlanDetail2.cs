using System;

namespace DotWeb.Models
{
    public partial class ControlPlanDetail2
    {
        public int Id { get; set; }
        public Nullable<int> ControlPlanDetail1Id { get; set; }
        public string ConsumptionMaterialNo { get; set; }
        public Nullable<int> MaterialId { get; set; }
        public Nullable<int> Qty { get; set; }
        public string RF { get; set; }
        public string QtyUnit { get; set; }
    }
}
