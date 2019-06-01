namespace ProjName_HostApp.Backend.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ProjName_HostApp.Backend.Entities;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public virtual DbSet<CrewMember> CrewMembers { get; set; }
        public virtual DbSet<ShipDesign> ShipDesigns { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>()
                .HasMany(e => e.CrewMembers)
                .WithOptional(e => e.Ship)
                .HasForeignKey(e => e.ShipRegistry);
        }
    }
}
