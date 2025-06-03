namespace GeoGuardian.Entities
{
    public class Address
    {
        public int    AddressId    { get; set; }  // ← chave primária
        public int    UserId       { get; set; }  // ← FK para User
        public int    CountryId    { get; set; }  
        public int    StateId      { get; set; }  
        public int    CityId       { get; set; }  
        public string Neighborhood { get; set; } = null!;
        public string StreetName   { get; set; } = null!;
        public string Number       { get; set; } = null!;  // ← número da residência
        public string? Complement  { get; set; }
        public decimal? Latitude   { get; set; }
        public decimal? Longitude  { get; set; }

        public User?    User     { get; set; }
        public Country? Country  { get; set; }
        public State?   State    { get; set; }
        public City?    City     { get; set; }
    }
}