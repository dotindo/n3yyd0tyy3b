using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("NotificationApps")]
    public class NotificationApp
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public int? OrganizationId { get; set; }
    }
}
