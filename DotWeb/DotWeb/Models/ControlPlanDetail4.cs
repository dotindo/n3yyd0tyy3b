using System;

namespace DotWeb.Models
{
    public partial class ControlPlanDetail4
    {
        public int id { get; set; }
        public Nullable<int> ControlPlanDetail1Id { get; set; }
        public string RF { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string DialogAddress { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public Nullable<bool> VDoc { get; set; }
        public Nullable<bool> JCBarcode { get; set; }
        public string Class { get; set; }
        public Nullable<bool> SecondHand { get; set; }
        public string DS { get; set; }
        public string DRT { get; set; }
        public string CS2 { get; set; }
        public string CS3 { get; set; }
        public string FBS3 { get; set; }
        public string SACode { get; set; }

        public virtual ControlPlanDetail1 ControlPlanDetail1 { get; set; }
    }
}
