using GeoGuardian.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Data;

public static class SeedData
{
    public static void Configure(ModelBuilder mb)
    {
        // ——— Tipos de Área de Risco ———
        mb.Entity<RiskAreaType>().HasData(
            new RiskAreaType { RiskAreaTypeId = 1, Name = "FLOOD" },
            new RiskAreaType { RiskAreaTypeId = 2, Name = "LANDSLIDE" },
            new RiskAreaType { RiskAreaTypeId = 3, Name = "DAM_BREAK" });

        // ——— Tipos de Alerta ———
        mb.Entity<AlertType>().HasData(
            new AlertType { AlertTypeId = 1, Name = "INFO" },
            new AlertType { AlertTypeId = 2, Name = "WARNING" },
            new AlertType { AlertTypeId = 3, Name = "CRITICAL" });

        // ——— Modelos de Sensor ———
        mb.Entity<SensorModel>().HasData(
            new SensorModel { SensorModelId = 1, Name = "ULTRASONIC_WL-X100", Maker = "Acme" },
            new SensorModel { SensorModelId = 2, Name = "SOILMOIST-S200",   Maker = "Acme" });

        // ——— Tipos de Usuário ———
        mb.Entity<UserType>().HasData(
            new UserType { UserTypeId = 1, Name = "ADMIN" },
            new UserType { UserTypeId = 2, Name = "USER"  });

        // ——— País ———
        mb.Entity<Country>().HasData(
            new Country { CountryId = 1, Name = "Brasil" });

        // ——— Estados e Cidades ———
        var estados = new (int Id, string Nome, string[] Cidades)[] {
            (1, "Acre", new[] { "Rio Branco", "Cruzeiro do Sul", "Sena Madureira" }),
            (2, "Alagoas", new[] { "Maceió", "Arapiraca", "Palmeira dos Índios" }),
            (3, "Amapá", new[] { "Macapá", "Santana", "Laranjal do Jari" }),
            (4, "Amazonas", new[] { "Manaus", "Parintins", "Itacoatiara" }),
            (5, "Bahia", new[] { "Salvador", "Feira de Santana", "Vitória da Conquista" }),
            (6, "Ceará", new[] { "Fortaleza", "Juazeiro do Norte", "Sobral" }),
            (7, "Distrito Federal", new[] { "Brasília", "Taguatinga", "Ceilândia" }),
            (8, "Espírito Santo", new[] { "Vitória", "Vila Velha", "Serra" }),
            (9, "Goiás", new[] { "Goiânia", "Anápolis", "Aparecida de Goiânia" }),
            (10, "Maranhão", new[] { "São Luís", "Imperatriz", "Caxias" }),
            (11, "Mato Grosso", new[] { "Cuiabá", "Várzea Grande", "Rondonópolis" }),
            (12, "Mato Grosso do Sul", new[] { "Campo Grande", "Dourados", "Três Lagoas" }),
            (13, "Minas Gerais", new[] { "Belo Horizonte", "Uberlândia", "Contagem" }),
            (14, "Pará", new[] { "Belém", "Santarém", "Marabá" }),
            (15, "Paraíba", new[] { "João Pessoa", "Campina Grande", "Patos" }),
            (16, "Paraná", new[] { "Curitiba", "Londrina", "Maringá" }),
            (17, "Pernambuco", new[] { "Recife", "Olinda", "Petrolina" }),
            (18, "Piauí", new[] { "Teresina", "Parnaíba", "Picos" }),
            (19, "Rio de Janeiro", new[] { "Rio de Janeiro", "Niterói", "Nova Iguaçu" }),
            (20, "Rio Grande do Norte", new[] { "Natal", "Mossoró", "Parnamirim" }),
            (21, "Rio Grande do Sul", new[] { "Porto Alegre", "Caxias do Sul", "Pelotas" }),
            (22, "Rondônia", new[] { "Porto Velho", "Ji-Paraná", "Ariquemes" }),
            (23, "Roraima", new[] { "Boa Vista", "Rorainópolis", "Caracaraí" }),
            (24, "Santa Catarina", new[] { "Florianópolis", "Joinville", "Blumenau" }),
            (25, "São Paulo", new[] { "São Paulo", "Campinas", "Santos" }),
            (26, "Sergipe", new[] { "Aracaju", "Nossa Senhora do Socorro", "Itabaiana" }),
            (27, "Tocantins", new[] { "Palmas", "Araguaína", "Gurupi" })
        };

        var cidades = new List<City>();
        int cityId = 1;

        foreach (var (stateId, nome, listaCidades) in estados)
        {
            mb.Entity<State>().HasData(new State { StateId = stateId, Name = nome, CountryId = 1 });

            foreach (var nomeCidade in listaCidades)
            {
                cidades.Add(new City { CityId = cityId++, Name = nomeCidade, StateId = stateId });
            }
        }

        mb.Entity<City>().HasData(cidades);

        // ——— Usuário ADMIN ———
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
