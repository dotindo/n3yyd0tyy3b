using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb
{
    public class User
    {
        [Key, MaxLength(128)]
        public string Id { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(LastName))
                    return FirstName;
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string OrganizationCode { get; set; }
        public virtual Organization Organization { get; set; }

        [MaxLength(50)]
        public string AuthKey { get; set; }
    }
}
