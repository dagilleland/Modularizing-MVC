namespace ProjName_HostApp.Backend.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpaceFleet.CrewMembers")]
    public partial class CrewMember
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Rank { get; set; }

        public int Division { get; set; }

        [StringLength(128)]
        public string ShipRegistry { get; set; }

        public virtual Ship Ship { get; set; }
    }
}
