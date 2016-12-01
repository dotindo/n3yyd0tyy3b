using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public partial class Modules_tmp
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ModuleType { get; set; }
        public string ScaffoldEntity { get; set; }
        public string TableName { get; set; }
        public int OrderNo { get; set; }
        public string Url { get; set; }
        public bool ShowInLeftMenu { get; set; }
        public int GroupId { get; set; }
    }
}
