using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shipyards.UI.Areas.Fleet.Models
{
    public class ShipyardRepository : DbContext
    {
        public ShipyardRepository() : base("name=DefaultConnection")
        { }

        public DbSet<ShipDesign> Designs { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
    }
    [Table("ShipDesigns", Schema = "SpaceFleet")]
    public class ShipDesign
    {
        [Key]
        public int ShipDesignId { get; set; }

        public string Name { get; set; }
        public DateTime CommissionedDate { get; set; }
        public int StandardCrewComplement { get; set; }

        public virtual ICollection<Ship> CommissionedShips { get; set; } =
            new HashSet<Ship>();
    }

    [Table("Ships", Schema = "SpaceFleet")]
    public class Ship
    {
        [Key]
        public string RegistryNumber { get; set; }
        public string Name { get; set; }
        public string LaunchDate { get; set; }

        [ForeignKey("Class")]
        public int ShipDesignId { get; set; }
        public virtual ShipDesign Class { get; set; }
        public virtual ICollection<CrewMember> Crew { get; set; } =
            new HashSet<CrewMember>();            
    }

    [Table("CrewMembers", Schema = "SpaceFleet")]
    public class CrewMember
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public FleetRank Rank { get; set; }
        public Division Division { get; set; }

        [ForeignKey("Assignment")]
        public string ShipRegistry { get; set; }

        public virtual Ship Assignment { get; set; }
    }

    [Flags]
    public enum Division
    {
        Operations = 1,
        Medical = 2,
        Science = 4,
        Command = 8,
        Section31 = 16
    }
    public enum FleetRank
    {
        Admiral,
        ViceAdmiral,
        RearAdmiral,
        Commodore,
        Captain,
        Commander,
        LieutenantCommander,
        Lieutenant,
        LieutenantJuniorGrade,
        Ensign,
        Crewman,
        Cadet
    }
}