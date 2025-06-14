﻿namespace GeoGuardian.Entities;

public class State 
{
    public int StateId   { get; set; }
    public string Name   { get; set; } = null!;
    public int CountryId { get; set; }

    public Country? Country { get; set; }
    public ICollection<City> Cities { get; set; } = [];
}
