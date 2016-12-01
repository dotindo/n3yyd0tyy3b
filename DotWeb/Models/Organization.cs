using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb
{
    public class Organization
    {
        [Key, MaxLength(20)]
        public string Code { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
