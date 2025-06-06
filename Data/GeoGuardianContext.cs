using GeoGuardian.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GeoGuardian.Data
{
    public class GeoGuardianContext : DbContext
    {
        public GeoGuardianContext(DbContextOptions<GeoGuardianContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserType> UserTypes => Set<UserType>();

        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<State> States => Set<State>();
        public DbSet<City> Cities => Set<City>();

        public DbSet<RiskAreaType> RiskAreaTypes => Set<RiskAreaType>();
        public DbSet<RiskArea> RiskAreas => Set<RiskArea>();

        public DbSet<SensorModel> SensorModels => Set<SensorModel>();
        public DbSet<Sensor> Sensors => Set<Sensor>();
        public DbSet<SensorReading> SensorReadings => Set<SensorReading>();

        public DbSet<AlertType> AlertTypes => Set<AlertType>();
        public DbSet<Alert> Alerts => Set<Alert>();
        public DbSet<UserAlert> UserAlerts => Set<UserAlert>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<UserAlert>()
                .HasKey(ua => new { ua.UserId, ua.AlertId });

            mb.Entity<UserAlert>()
                .Property(ua => ua.ConfirmedReception)
                .HasConversion<int>()
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            mb.Entity<Address>()
                .Property(a => a.Latitude)
                .HasColumnType("NUMBER(9,6)");
            mb.Entity<Address>()
                .Property(a => a.Longitude)
                .HasColumnType("NUMBER(9,6)");

            mb.Entity<SensorReading>()
                .Property(sr => sr.Value)
                .HasPrecision(18, 3);

            mb.Entity<Sensor>()
                .Property(s => s.Uuid)
                .HasMaxLength(36)
                .IsRequired();

            mb.Entity<Address>()
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(a => a.CountryId);

            mb.Entity<Address>()
                .HasOne<State>()
                .WithMany()
                .HasForeignKey(a => a.StateId);

            mb.Entity<Address>()
                .HasOne<City>()
                .WithMany()
                .HasForeignKey(a => a.CityId);

            mb.Entity<UserType>()
                .HasMany(ut => ut.Users)
                .WithOne(u => u.UserType)
                .HasForeignKey(u => u.UserTypeId);

            mb.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
            
            mb.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            mb.Entity<Country>()
                .HasMany(c => c.States)
                .WithOne(s => s.Country)
                .HasForeignKey(s => s.CountryId);

            mb.Entity<State>()
                .HasMany(s => s.Cities)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId);

            mb.Entity<RiskAreaType>()
                .HasMany(rt => rt.RiskAreas)
                .WithOne(r => r.RiskAreaType)
                .HasForeignKey(r => r.RiskAreaTypeId);

            mb.Entity<City>()
                .HasMany(c => c.RiskAreas)
                .WithOne(r => r.City)
                .HasForeignKey(r => r.CityId);

            mb.Entity<RiskArea>()
                .HasMany(r => r.Sensors)
                .WithOne(s => s.RiskArea)
                .HasForeignKey(s => s.RiskAreaId);

            mb.Entity<SensorModel>()
                .HasMany(sm => sm.Sensors)
                .WithOne(s => s.SensorModel)
                .HasForeignKey(s => s.SensorModelId);

            mb.Entity<Sensor>()
                .HasMany(s => s.Readings)
                .WithOne(r => r.Sensor)
                .HasForeignKey(r => r.SensorId);

            mb.Entity<AlertType>()
                .HasMany(at => at.Alerts)
                .WithOne(a => a.AlertType)
                .HasForeignKey(a => a.AlertTypeId);

            mb.Entity<RiskArea>()
                .HasMany(r => r.Alerts)
                .WithOne(a => a.RiskArea)
                .HasForeignKey(a => a.RiskAreaId);

            mb.Entity<UserAlert>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAlerts)
                .HasForeignKey(ua => ua.UserId);

            mb.Entity<UserAlert>()
                .HasOne(ua => ua.Alert)
                .WithMany(a => a.UserAlerts)
                .HasForeignKey(ua => ua.AlertId);
            
            mb.Entity<Alert>()
                .HasOne(a => a.Address)
                .WithMany(ad => ad.Alerts)
                .HasForeignKey(a => a.AddressId);
            
            mb.Entity<Alert>()
                .HasOne(a => a.User)
                .WithMany(u => u.Alerts)
                .HasForeignKey(a => a.UserId);



            SeedData.Configure(mb);
        }
    }

    public class GeoGuardianDesignTimeContextFactory : IDesignTimeDbContextFactory<GeoGuardianContext>
    {
        public GeoGuardianContext CreateDbContext(string[] args)
        {
            var cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var opts = new DbContextOptionsBuilder<GeoGuardianContext>()
                .UseOracle(cfg.GetConnectionString("Oracle"))
                .Options;

            return new GeoGuardianContext(opts);
        }
    }
}
