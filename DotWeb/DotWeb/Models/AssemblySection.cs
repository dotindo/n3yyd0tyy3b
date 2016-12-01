using System.Collections.Generic;

namespace DotWeb.Models
{
    public partial class AssemblySection
    {
        public AssemblySection()
        {
            CgisFilters = new HashSet<CGISFilter>();    
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string AssemblySectionName { get; set; }

        public virtual ICollection<CGISFilter> CgisFilters { get; set; }
    }
}
