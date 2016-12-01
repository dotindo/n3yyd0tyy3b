using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("NotificationEmails")]
    public class NotificationEmail
    {
        public int Id { get; set; }
        public string SubjectEmail { get; set; }
        public string Email { get; set; }
        public string To { get; set; }
    }
}
