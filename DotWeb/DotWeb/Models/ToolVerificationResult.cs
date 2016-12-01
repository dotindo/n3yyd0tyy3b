using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public partial class ToolVerificationResult
    {
        public ToolVerificationResult()
        {
            ToolVerifications = new HashSet<ToolVerification>();
        }
        public int Id { get; set; } 
        public string Name { get; set; }
        public virtual ICollection<ToolVerification> ToolVerifications { get; set; }
    }
}
