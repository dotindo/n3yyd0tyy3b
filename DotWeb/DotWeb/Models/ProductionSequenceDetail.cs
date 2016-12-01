using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("ProductionSequenceDetail")]
    public partial class ProductionSequenceDetail
    {        
        public int Id { get; set; }
        public int ProductionSequenceId { get; set; }
        public string type { get; set; }
        public int CummFigr { get; set; }
        public string MaterialNo { get; set; }
        public string OrderNo { get; set; }
        public string SerialNumber { get; set; }
        public string DBProdSIFI { get; set; }
        public string CommnosNumber { get; set; }
        public int Lot { get; set; }
        public string EngineNo { get; set; }
        public int ColCode { get; set; }
        public int IntCode { get; set; }
        public string BomExpl { get; set; }
        public string ChassisNoDCAG { get; set; }
        public string FinishDate { get; set; }
        public virtual ProductionSequence ProductionSequence { get; set; }
    }
}
