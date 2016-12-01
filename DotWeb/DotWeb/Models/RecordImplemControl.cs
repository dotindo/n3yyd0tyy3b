using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("RecordImplemControl")]
    public class RecordImplemControl
    {
        public int Id { get; set; }
        public int Issuer { get; set; }
        public DateTime IssuedDate { get; set; }
        public int RICStatusId { get; set; }
        public string RICNR { get; set; }
        public string AlterationId { get; set; }
        public string ReasonOfAlteration { get; set; }
        public int CommnosCountry { get; set; }
        public int CommnosFrom { get; set; }
        public int CommnosTo { get; set; }
        public string PackingMonth { get; set; }
        public string ChassisNR { get; set; }
        public string ImplPlan { get; set; }
        public string CumulativeFigure { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime ActualImplementationDate { get; set; }
        public bool Remark { get; set; }
        public string Codes { get; set; }
        public DateTime OldUntil { get; set; }
        public DateTime NewFrom { get; set; }
        public int CreatedById { get; set; }
        public int ApprovedBy1 { get; set; }
        public DateTime ApprovedBy1Date { get; set; }
        public int ApprovedBy2 { get; set; }
        public DateTime ApprovedBy2Date { get; set; }
        public int ApprovedBy3 { get; set; }
        public DateTime ApprovedBy3Date { get; set; }
        public int ApprovedBy4 { get; set; }
        public DateTime ApprovedBy4Date { get; set; }
        public int ModelId { get; set; }
        public int VarianId { get; set; }
    }
}