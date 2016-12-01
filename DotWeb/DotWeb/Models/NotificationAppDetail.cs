using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("NotificationAppDetails")]
    public class NotificationAppDetail
    {
        public int Id { get; set; }
        public int NotificationAppId { get; set; }
        public string OrganizationCode { get; set; }
        public string UserGroupMember { get; set; }
        public string UserId { get; set; }
    }
}
