using AngelStack.Common.Guards;
using AngelStack.Common.Strings;
using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.ValueObjects;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;

internal static class DataLoader
{
    public static IEnumerable<Country> LoadCountries()
    {
        var countriesJson = ParseJsonArrayFromFile<CountryJson>("countries.json");

        var countries = countriesJson?.Select(c =>
        {
            var name = new CountryName(c.Name);
            return new Country(name);
        });

        return countries ?? [];
    }

    public static IEnumerable<RegionType> LoadRegionTypes()
    {
        return [
            new RegionType(new RegionTypeName("Geographic")),
            new RegionType(new RegionTypeName("State")),
        ];
    }

    public static IEnumerable<Region> LoadBrazilRegions(
        IEnumerable<Country> countries,
        IEnumerable<RegionType> types)
    {
        var regionsJson = (ParseJsonArrayFromFile<RegionJson>("brazil-regions.json") ?? []).ToList();
        var states = ParseJsonArrayFromFile<RegionJson>("brazil-states.json") ?? [];
        regionsJson.AddRange(states);
        var country = countries.FirstOrDefault(c => c.Name.Value.Equals("Brazil")).Guard();
        List<Region> regions = [];

        foreach(var data in regionsJson)
        {
            // TODO add iso properties to country
            // TODO override equals of stringvalue

            var type = types.FirstOrDefault(t => t.Name.Value.Equals(data.Type)).Guard();
            var name = new RegionName(data.Name);
            var alias = data.Alias is not null ? new RegionAlias(data.Alias) : null;
            var parent = regions.FirstOrDefault(r => r.Name.Value.Equals(data.Region));
            var region = new Region(country, type, name, alias, parent);

            regions.Add(region);
        }

        return regions ?? [];
    }

    public static IEnumerable<City> LoadBrazilCities(IEnumerable<Region> regions)
    {
        var citiesJson = ParseJsonArrayFromFile<CityJson>("brazil-cities.json") ?? [];
        var cities = citiesJson.Select(data =>
        {
            var name = new CityName(data.Name);
            var region = regions.FirstOrDefault(r => data.Region.Equals(r.Alias?.Value)).Guard();

            return new City(region, name);
        });

        return cities;
    }

    private static string ReadFromJson(string path)
    {
        return File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "Json", path));
    }

    private static T? ParseJsonFromFile<T>(string path)
    {
        return ReadFromJson(path).ParseJson<T>();
    }

    private static IEnumerable<T>? ParseJsonArrayFromFile<T>(string path)
    {
        return ParseJsonFromFile<IEnumerable<T>>(path);
    }
}
