using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{    
    public partial class ProductionSequence
    {
        public ProductionSequence()
        {
            ProductionSequenceDetails = new HashSet<ProductionSequenceDetail>();
        }
        public int Id { get; set; }
        public int PackingMonth { get; set; }
        public int ModelId { get; set; }
        public int VarianId { get; set; }
        public int FileType{get;set;}
        public virtual ICollection<ProductionSequenceDetail> ProductionSequenceDetails { get; set; }
    }
}
