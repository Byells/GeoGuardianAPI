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

        // ——— DbSets para todas as entidades efetivamente usadas ———
        public DbSet<User>          Users          => Set<User>();
        public DbSet<UserType>      UserTypes      => Set<UserType>();

        public DbSet<Address>       Addresses      => Set<Address>();
        public DbSet<Country>       Countries      => Set<Country>();
        public DbSet<State>         States         => Set<State>();
        public DbSet<City>          Cities         => Set<City>();
        public DbSet<Neighbourhood> Neighbourhoods => Set<Neighbourhood>();
        public DbSet<Street>        Streets        => Set<Street>();

        public DbSet<RiskAreaType>  RiskAreaTypes  => Set<RiskAreaType>();
        public DbSet<RiskArea>      RiskAreas      => Set<RiskArea>();

        public DbSet<SensorModel>   SensorModels   => Set<SensorModel>();
        public DbSet<Sensor>        Sensors        => Set<Sensor>();
        public DbSet<SensorReading> SensorReadings => Set<SensorReading>();

        public DbSet<AlertType>     AlertTypes     => Set<AlertType>();
        public DbSet<Alert>         Alerts         => Set<Alert>();
        public DbSet<UserAlert>     UserAlerts     => Set<UserAlert>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // ——— Chave composta de UserAlert ———
            mb.Entity<UserAlert>()
                .HasKey(ua => new { ua.UserId, ua.AlertId });

            // ——— Configuração de conversão/precision de colunas ———
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

            // ——— Relacionamentos ———

            // UserType 1-N User
            mb.Entity<UserType>()
                .HasMany(ut => ut.Users)
                .WithOne(u => u.UserType)
                .HasForeignKey(u => u.UserTypeId);

            // User 1-N Address
            mb.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // Country 1-N State
            mb.Entity<Country>()
                .HasMany(c => c.States)
                .WithOne(s => s.Country)
                .HasForeignKey(s => s.CountryId);

            // State 1-N City
            mb.Entity<State>()
                .HasMany(s => s.Cities)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId);

            // City 1-N Neighbourhood
            mb.Entity<City>()
                .HasMany(c => c.Neighbourhoods)
                .WithOne(n => n.City)
                .HasForeignKey(n => n.CityId);

            // Neighbourhood 1-N Street
            mb.Entity<Neighbourhood>()
                .HasMany(n => n.Streets)
                .WithOne(s => s.Neighbourhood)
                .HasForeignKey(s => s.NeighbourhoodId);

            // Street 1-N RiskArea
            mb.Entity<Street>()
                .HasMany(s => s.RiskAreas)
                .WithOne(r => r.Street)
                .HasForeignKey(r => r.StreetId);

            // RiskAreaType 1-N RiskArea
            mb.Entity<RiskAreaType>()
                .HasMany(rt => rt.RiskAreas)
                .WithOne(r => r.RiskAreaType)
                .HasForeignKey(r => r.RiskAreaTypeId);

            // RiskArea 1-N Sensor
            mb.Entity<RiskArea>()
                .HasMany(r => r.Sensors)
                .WithOne(s => s.RiskArea)
                .HasForeignKey(s => s.RiskAreaId);

            // SensorModel 1-N Sensor
            mb.Entity<SensorModel>()
                .HasMany(sm => sm.Sensors)
                .WithOne(s => s.SensorModel)
                .HasForeignKey(s => s.SensorModelId);

            // Sensor 1-N SensorReading
            mb.Entity<Sensor>()
                .HasMany(s => s.Readings)
                .WithOne(r => r.Sensor)
                .HasForeignKey(r => r.SensorId);

            // AlertType 1-N Alert
            mb.Entity<AlertType>()
                .HasMany(at => at.Alerts)
                .WithOne(a => a.AlertType)
                .HasForeignKey(a => a.AlertTypeId);

            // RiskArea 1-N Alert
            mb.Entity<RiskArea>()
                .HasMany(r => r.Alerts)
                .WithOne(a => a.RiskArea)
                .HasForeignKey(a => a.RiskAreaId);

            // UserAlert N-N
            mb.Entity<UserAlert>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAlerts)
                .HasForeignKey(ua => ua.UserId);

            mb.Entity<UserAlert>()
                .HasOne(ua => ua.Alert)
                .WithMany(a => a.UserAlerts)
                .HasForeignKey(ua => ua.AlertId);

            // ——— Seed data (se existir) ———
            SeedData.Configure(mb);
        }
    }

    // ——— Factory para dotnet-ef ———
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
