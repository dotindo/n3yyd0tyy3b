using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("SMTPConfigs")]
    public class SMTPConfig
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Port { get; set; }
        public bool IsUseSSL { get; set; }
    }
}
