using System;
using System.Collections.Generic;
using System.Text;
using BnHomeAllocationApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BNHomeAllocation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }



        //Join Table for Many to Many Relationships between Models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ResidenceZone>().HasData(
              new ResidenceZone() { Id = 1, Name = "NHQ Residential Area" },
              new ResidenceZone() { Id = 2, Name = "ShaheenBag" },
              new ResidenceZone() { Id = 3, Name = "Nakhal Para Dhaka" }
              );


            modelBuilder.Entity<ResidenceBuilding>().HasData(
           new ResidenceBuilding() { Id = 1, Name = "Bungalow", ResidenceZoneId = 1, BuildingTypeId = 1 },
           new ResidenceBuilding() { Id = 2, Name = "10 Storied Building No -108", ResidenceZoneId = 1, BuildingTypeId = 2 },
           new ResidenceBuilding() { Id = 3, Name = "121 B-1", ResidenceZoneId = 2, BuildingTypeId = 2 }
           );

            modelBuilder.Entity<Residence>().HasData(
          new Residence() { Id = 1, Name = "Bungalow A/1", ResidenceBuildingId = 1 },
          new Residence() { Id = 2, Name = "Bungalow A/2", ResidenceBuildingId = 1 },
          new Residence() { Id = 3, Name = "Bungalow A/3", ResidenceBuildingId = 1 },
          new Residence() { Id = 4, Name = "108 B/1", ResidenceBuildingId = 2 },
          new Residence() { Id = 5, Name = "108 B/2", ResidenceBuildingId = 2 },
          new Residence() { Id = 6, Name = "108 B/3", ResidenceBuildingId = 2 }
          );
            modelBuilder.Entity<OfficerRank>().HasData(
         new OfficerRank() { Id = 1, Name = "Admiral", RankPoint = 0 },
         new OfficerRank() { Id = 2, Name = "Vice Admiral", RankPoint = 0 },
         new OfficerRank() { Id = 3, Name = "Rear Admiral", RankPoint = 15 },
         new OfficerRank() { Id = 4, Name = "Commodore", RankPoint = 9 },
         new OfficerRank() { Id = 5, Name = "Captain", RankPoint = 7 },
         new OfficerRank() { Id = 6, Name = "Commander", RankPoint = 5 },
         new OfficerRank() { Id = 7, Name = "Lieutenant Commander", RankPoint = 3 },
         new OfficerRank() { Id = 8, Name = "Lieutenant", RankPoint = 0 },
         new OfficerRank() { Id = 9, Name = "Sub Lieutenant", RankPoint = 0 }
         );
            modelBuilder.Entity<ApplicationType>().HasData(
       new ApplicationType() { Id = 1, Name = "General" },
       new ApplicationType() { Id = 2, Name = "Special Consideration" },
       new ApplicationType() { Id = 3, Name = "NAM" }
       );


            modelBuilder.Entity<BuildingType>().HasData(
     new BuildingType() { Id = 1, Name = "A Type" },
     new BuildingType() { Id = 2, Name = "B Type" },
     new BuildingType() { Id = 3, Name = "C Type" }
     );

        }


        public DbSet<Officer> Officers { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<OfficerRank> OfficerRanks { get; set; }
        public DbSet<OfficerResidenceInfo> OfficerResidenceInfoes { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<ResidenceBuilding> ResidenceBuildings { get; set; }
        public DbSet<ResidenceZone> ResidenceZones { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }

    }
}
