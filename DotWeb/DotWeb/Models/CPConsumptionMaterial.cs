using System;

namespace DotWeb.Models
{
    public partial class CPConsumptionMaterial
    {
        public int Id { get; set; }
        public Nullable<int> CPDetailId { get; set; }
        public string PartNo { get; set; }
        public string MaterialDescription { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string Unity { get; set; }
        public string LastAlterationPM { get; set; }
        public string Status { get; set; }
        public string Class { get; set; }
        public string TDS { get; set; }
        public string MSDS { get; set; }
        public string Hazard { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string Remarks { get; set; }
        public string DBL { get; set; }
        public string SupportingDoc { get; set; }
        public string VehicleTypeImpacted { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }

        public virtual CPDetail CPDetail { get; set; }
    }
}
