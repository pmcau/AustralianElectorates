using Aspose.Gis;
using Aspose.Gis.Rendering;
using Aspose.Gis.Rendering.Symbolizers;
using Aspose.Gis.SpatialReferencing;
using NetTopologySuite.Algorithm.Locate;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Feature = NetTopologySuite.Features.Feature;

public class ConvertToSqlGeometryTests
{
    private readonly ITestOutputHelper output;

    public ConvertToSqlGeometryTests(ITestOutputHelper output) =>
        this.output = output;

    [Fact]
    public void Foo()
    {
        Dictionary<string, IPointOnGeometryLocator> locators = new Dictionary<string, IPointOnGeometryLocator>();
        var electorateDir = Path.Combine(DataLocations.Maps2022Path, "Electorates");
        foreach (var path in Directory.EnumerateFiles(electorateDir, "*.geojson").Where(_ => _.Contains("_20")))
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

    [Fact]
    public void Bar()
    {
        var locators = new Dictionary<string, IPointOnGeometryLocator>();
        var geojsonFile = Path.Combine(DataLocations.MapsPath, "2022", "australia.geojson");
        var serializer = GeoJsonSerializer.Create(new GeometryFactoryEx());
        using var jsonReader = new JsonTextReader(File.OpenText(geojsonFile));
        var featureCollection = serializer.Deserialize<FeatureCollection>(jsonReader);
        foreach (var feature in featureCollection)
        {
            var geometry = feature.Geometry;
            var featureAttributes = feature.Attributes;
            locators.Add(featureAttributes["electorateName"].ToString()!, new IndexedPointInAreaLocator(geometry));
        }

        var startNew = Stopwatch.StartNew();
        foreach (var locator in locators)
        {
            var location = locator.Value.Locate(new(149.09, -35.349));

            var isInside = !location.HasFlag(Location.Exterior);
            if (isInside)
            {
                output.WriteLine(locator.Key);
            }
        }

        output.WriteLine(startNew.ElapsedMilliseconds.ToString());
    }

    [Fact]
    public void AddLabel()
    {
         File.Delete("a.png");
        var geojsonFile = Path.Combine(DataLocations.MapsCuratedPath,@"2022\Electorates\durack.geojson");
        using var map = new Map(800,800);
        
        var symbolizer = new SimpleLine
            {Width = Measurement.Pixels(2)};
        map.SpatialReferenceSystem = SpatialReferenceSystem.Wgs84;
        map.Add(VectorLayer.Open(geojsonFile, Drivers.GeoJson), symbolizer);
        map.Render("a.png", Renderers.Png);
    }
}