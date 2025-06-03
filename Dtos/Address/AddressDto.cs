namespace GeoGuardian.Dtos.Address
{
    public class AddressDto
    {
        public int AddressId     { get; set; }
        public int UserId        { get; set; }

        public int CountryId     { get; set; }
        public int StateId       { get; set; }
        public int CityId        { get; set; }

        public string Neighborhood { get; set; } = null!;
        public string StreetName   { get; set; } = null!;
        public string? Number      { get; set; }         
        public string? Complement  { get; set; }

        public decimal? Latitude   { get; set; }
        public decimal? Longitude  { get; set; }
    }
}