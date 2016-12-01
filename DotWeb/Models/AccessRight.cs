using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb
{
    public enum PrincipalType
    {
        User = 1,
        Group = 2
    }

    public enum SecuredObjectType
    {
        App = 1,
        ModuleGroup = 2,
        Module = 3
    }

    public class AccessRight
    {
        public long Id { get; set; }

        [Required, MaxLength(128)]
        public string PrincipalId { get; set; }

        public PrincipalType PrincipalType { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int SecuredObjectId { get; set; }

        public SecuredObjectType SecuredObjectType { get; set; }
    }
}
