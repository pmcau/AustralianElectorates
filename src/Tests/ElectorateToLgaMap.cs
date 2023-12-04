using AustralianElectorates;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

public class ElectorateToLgaMap
{
    [Fact]
    public void ElectorateOverlapWithLga()
    {
        var factory = new GeometryFactory();

        using var shapeFileDataReader = new ShapefileDataReader(Path.Combine(AttributeReader.GetProjectDirectory(), @"Lga\LGA_2022_AUST_GDA2020.shp"), factory);

        var electorateToLga = new Dictionary<string, List<string>>();
        var lgaToElectorate = new Dictionary<string, List<string>>();

        var electorates = DataLoader.Electorates
            .Where(_ => _.Exist2022)
            .Select(
                _ =>
                {
                    var serializer = GeoJsonSerializer.Create(new GeometryFactoryEx());
                    using var jsonReader = new JsonTextReader(new StringReader(_.Get2022Map().GeoJson));
                    var featureCollection = serializer.Deserialize<FeatureCollection>(jsonReader)!;
                    var feature = featureCollection.Single();
                    return new
                    {
                        _.Name,
                        feature.Geometry
                    };
                })
            .ToList();

        foreach (var electorate in electorates)
        {
            electorateToLga[electorate.Name] = [];
        }

        while (shapeFileDataReader.Read())
        {
            var lgaGeometry = shapeFileDataReader.Geometry;
            var lga = shapeFileDataReader.GetString(2);

            var list = new List<string>();
            lgaToElectorate.Add(lga, list);
            foreach (var electorate in electorates)
            {
                if (lgaGeometry.Intersects(electorate.Geometry))
                {
                    list.Add(electorate.Name);
                    electorateToLga[electorate.Name].Add(lga);
                }
            }
        }

        electorateToLga["Lingiari"].Add("Cocos Islands");
        lgaToElectorate["Cocos Islands"].Add("Lingiari");

        File.Delete(DataLocations.LgaToElectorateJsonPath);
        File.Delete(DataLocations.ElectorateToLgaJsonPath);
        File.WriteAllText(DataLocations.LgaToElectorateJsonPath, JsonConvert.SerializeObject(lgaToElectorate, Formatting.Indented));
        File.WriteAllText(DataLocations.ElectorateToLgaJsonPath, JsonConvert.SerializeObject(electorateToLga, Formatting.Indented));
    }
}