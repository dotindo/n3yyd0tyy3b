using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotWeb.Models
{
    [Table("Vehicleorders")]
    public class VehicleOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
         public VehicleOrders()
        {
            this.VehicleOrderDetails = new HashSet<VehicleOrderDetails>();
        }
    
        public int Id { get; set; }
        public string PackingMonth { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> VarianId { get; set; }
        public Nullable<int> FileType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleOrderDetails> VehicleOrderDetails { get; set; }
    }
}
