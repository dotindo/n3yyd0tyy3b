using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public class IATask
    {
        public class IATaskHeader : IATaskStatus
        {
            public int IAHeaderId { get; set; }
            public int UserGroupId { get; set; }
            public int AssignedUserId { get; set; }
            public DateTime AssignedDate { get; set; }
            public int DelegateUserId { get; set; }
            public DateTime DelegateDate { get; set; }
            public string Task { get; set; }
            public int IATaskStatusId { get; set; }
            public List<IATaskReports> TaskReports { get; set; }
        }

        public class IATaskReports
        { 
           public int IATaskStatusId { get; set; }
	       public int IATaskId { get; set; }
	       public string Report { get; set; }
           public int AttachmentId { get; set; }
        }
        public class IATaskStatus
        {
            public string Status { get; set; }
        }
    }
}
