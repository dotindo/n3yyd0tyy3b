using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("FileType")]
    public partial class FileType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
