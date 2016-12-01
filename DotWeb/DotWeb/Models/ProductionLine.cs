using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("ProductionLines")]
    public partial class ProductionLine
    {
        public int Id { get; set; }
        public string LineName { get; set; }
    }
}
