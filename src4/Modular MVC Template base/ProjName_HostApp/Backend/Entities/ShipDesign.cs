namespace ProjName_HostApp.Backend.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpaceFleet.ShipDesigns")]
    public partial class ShipDesign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShipDesign()
        {
            Ships = new HashSet<Ship>();
        }

        public int ShipDesignId { get; set; }

        public string Name { get; set; }

        public DateTime CommissionedDate { get; set; }

        public int StandardCrewComplement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ship> Ships { get; set; }
    }
}
