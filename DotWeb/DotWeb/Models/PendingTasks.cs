using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("PendingTasks")]
    public class PendingTasks
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Task { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public bool Pending { get; set; }
    }
}
