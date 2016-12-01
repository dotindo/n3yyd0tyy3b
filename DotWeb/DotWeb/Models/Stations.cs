using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotWeb.Models
{
    public partial class Stations
    {
        public Stations()
        {
            CgisFilters = new HashSet<CGISFilter>();    
        }

        public int Id { get; set; }

        public int AssemblySectionId { get; set; }

        [StringLength(2)]
        public string Code { get; set; }

        [StringLength(100)]
        public string StationName { get; set; }

        public virtual ICollection<CGISFilter> CgisFilters { get; set; }

        public virtual AssemblySection AssemblySection { get; set; }
    }
}
