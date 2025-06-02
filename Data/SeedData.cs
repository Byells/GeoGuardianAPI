using GeoGuardian.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Data;

public static class SeedData
{
    public static void Configure(ModelBuilder mb)
    {
        mb.Entity<RiskAreaType>().HasData(
            new RiskAreaType { RiskAreaTypeId = 1, Name = "FLOOD" },
            new RiskAreaType { RiskAreaTypeId = 2, Name = "LANDSLIDE" },
            new RiskAreaType { RiskAreaTypeId = 3, Name = "DAM_BREAK" });
        

        mb.Entity<AlertType>().HasData(
            new AlertType { AlertTypeId = 1, Name = "INFO" },
            new AlertType { AlertTypeId = 2, Name = "WARNING" },
            new AlertType { AlertTypeId = 3, Name = "CRITICAL" });

        mb.Entity<SensorModel>().HasData(
            new SensorModel { SensorModelId = 1, Name = "ULTRASONIC_WL-X100", Maker = "Acme" },
            new SensorModel { SensorModelId = 2, Name = "SOILMOIST-S200",   Maker = "Acme" });
        
        mb.Entity<UserType>().HasData(
            new UserType { UserTypeId = 1, Name = "ADMIN" },
            new UserType { UserTypeId = 2, Name = "USER"  });

        
        mb.Entity<Country>().HasData(
            new Country { CountryId = 1, Name = "Brasil" });

        mb.Entity<State>().HasData(
            new State { StateId = 1, Name = "São Paulo", CountryId = 1 });

        mb.Entity<City>().HasData(
            new City { CityId = 1, Name = "São Paulo", StateId = 1 });

        mb.Entity<Neighbourhood>().HasData(
            new Neighbourhood { NeighbourhoodId = 1, Name = "Bela Vista", CityId = 1 });

        mb.Entity<Street>().HasData(
            new Street { StreetId = 1, Name = "Av. Paulista", NeighbourhoodId = 1 });
        
        mb.Entity<User>().HasData(
            new User
            {
                UserId     = 1,
                FullName   = "Admin",
                Email      = "admin@geo.com",
                Password   = "Admingeo", 
                Created    =  new DateTime(2025, 6, 2, 0, 0, 0),
                UserTypeId = 1
            });
    }
}