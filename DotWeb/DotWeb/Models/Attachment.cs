using System;

namespace DotWeb.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public Nullable<int> DocTypeId { get; set; }
        public Nullable<int> DocId { get; set; }
        public string FileLocation { get; set; }
        public string FileType { get; set; }

        public virtual DocType DocType { get; set; }
    }
}
