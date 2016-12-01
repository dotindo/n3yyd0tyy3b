using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb
{
    public enum PermissionType
    {
        NoAccess = 0,
        Read = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,
        Print = 5,
        Admin = 9
    }

    public class Permission
    {
        public long Id { get; set; }

        public PermissionType PermissionType { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
