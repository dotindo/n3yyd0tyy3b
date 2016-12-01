using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb.Models
{
    public class IAHeaders
    {
        public int IAId { get; set; }
        public string IAType { get; set; }
        public string InternalEpcNumber { get; set; }
        public string InfoNumber { get; set; }
        public string InfoFrom { get; set; }
        public string Description { get; set; }
        public DateTime? ValidPeriodFrom { get; set; }
        public DateTime? ValidPeriodTo { get; set; }
        public DateTime? ImplementationDate { get; set; }
        public int AuthorUserId { get; set; }
        public int ApproverUserId { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string StatusID { get; set; }
        public bool DynamicCheck { get; set; }
        public List<IAModels> iaModels { get; set; }
        public DataTable DtIaModels { get; set; }
        public List<IAPackingMonths> iaPackingMonth { get; set; }
        public DataTable DtIaPackingMonth { get; set; }
        public List<IAParts> iaParts { get; set; }
        public DataTable DtIaParts { get; set; }
        public List<IAStations> iaStations { get; set; }
        public DataTable DtIaStations { get; set; }
        public List<IATask> iatask { get; set; }
        public DataTable DtIaTask { get; set; }

    }
    public class IAModel
    {
        public int IDModel { get; set; }
        public string ModelName { get; set; }
    }
    public class IAStatusTask
    {
        public int IdStatusTask { get; set; }
        public string StatusTask { get; set; }
    }
    public class IATaskApproval
    {
        public int IdTaskApproval { get; set; }
        public string NameApprovalTask { get; set; }
    }
    public class IATaskDepartment
    {
        public int IdTaskDepartment { get; set; }
        public string NameTaskDepartment { get; set; }
    }
    public class IAAttachment
    {
        public int IdAttachment { get; set; }
        public string NameAttachment { get; set; }
        public string LocAttachement { get; set; }
    }
}
