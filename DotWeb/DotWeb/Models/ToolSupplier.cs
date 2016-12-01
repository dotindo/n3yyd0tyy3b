using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public partial class ToolSupplier
    {
        public ToolSupplier()
        {
            this.Tools = new HashSet<Tool>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }
    }
}
