using System;

namespace DotWeb.Models
{
    public partial class ControlPlanStation
    {
        public int Id { get; set; }
        public Nullable<int> ControlPlanId { get; set; }
        public Nullable<int> StationId { get; set; }
    }
}
