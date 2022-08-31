using NetTopologySuite.Algorithm.Locate;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Xunit.Abstractions;

public class ConvertToSqlGeometryTests
{
    private readonly ITestOutputHelper output;

    public ConvertToSqlGeometryTests(ITestOutputHelper output)
    {
        this.output = output;
    }
    [Fact]
    public void Foo()
    {
        Dictionary<string, IPointOnGeometryLocator> locators = new Dictionary<string, IPointOnGeometryLocator>();
        var electorateDir = Path.Combine(DataLocations.Maps2022Path,"Electorates");
        foreach (var path in Directory.EnumerateFiles(electorateDir,"*.geojson").Where(_=>_.Contains("_20")))
        {
            var serializer = GeoJsonSerializer.Create(new GeometryFactoryEx());
            using var jsonReader = new JsonTextReader(File.OpenText(path));
            var featureCollection = serializer.Deserialize<FeatureCollection>(jsonReader);
            var feature = featureCollection.Single();
            var geometry = feature.Geometry;
            locators.Add(path, new IndexedPointInAreaLocator(geometry));
        }

        var startNew = Stopwatch.StartNew();
        foreach (var locator in locators)
        {
            var location = locator.Value.Locate(new(14.09, -35.349));

            var isInside = !location.HasFlag(Location.Exterior);
            if (isInside)
            {
                output.WriteLine(locator.Key);
            }
        }
        output.WriteLine(startNew.ElapsedMilliseconds.ToString());
    }

}