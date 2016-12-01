using System;

namespace DotWeb.Utils
{
    public class PKInfo
    {
        public string ConstraintName { get; set; }

        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public bool IsIdentity { get; set; }
    }
}
