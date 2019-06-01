namespace ProjName_HostApp.Backend.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpaceFleet.Ships")]
    public partial class Ship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ship()
        {
            CrewMembers = new HashSet<CrewMember>();
        }

        [Key]
        public string RegistryNumber { get; set; }

        public string Name { get; set; }

        public string LaunchDate { get; set; }

        public int ShipDesignId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CrewMember> CrewMembers { get; set; }

        public virtual ShipDesign ShipDesign { get; set; }
    }
}
