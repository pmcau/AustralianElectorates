using System.Text.Json;
using Aspose.Gis;
using Aspose.Gis.Geometries;
using Aspose.Gis.Rendering;
using Aspose.Gis.Rendering.Labelings;
using Aspose.Gis.Rendering.Symbolizers;
using Aspose.Gis.SpatialReferencing;
using AustralianElectorates;
using NetTopologySuite.Features;
using Feature = NetTopologySuite.Features.Feature;

[UsesVerify]
public class AsposeTests
{
    // significant urban areas https://www.abs.gov.au/statistics/standards/australian-statistical-geography-standard-asgs-edition-3/jul2021-jun2026/access-and-downloads/allocation-files
    // Use: Significant Urban Areas - 2021
    // population https://www.abs.gov.au/methodologies/data-region-methodology/2015-20#data-download
    // https://www.abs.gov.au/statistics/people/population/regional-population/2021
    // Population estimates by Significant Urban Area and Remoteness Area (ASGS2016), 2001 to 2021
    // Significant Urban Areas, Urban Centres and Localities, Section of State and Section of State Range 
    // https://www.abs.gov.au/statistics/standards/australian-statistical-geography-standard-asgs-edition-3/jul2021-jun2026/access-and-downloads/digital-boundary-files
    [Fact]
    public void AddLabel()
    {
        var absDir = Path.Combine(AttributeReader.GetProjectDirectory(), "ABS");

        File.Delete("a.png");
        var geojsonFile = Path.Combine(DataLocations.MapsCuratedPath, @"2022\Electorates\durack.geojson");

        using var map = new Map(1024, 1024);
        //17.9618° S, 122.2370° E

        var symbolizer = new SimpleLine
        {
            Width = Measurement.Pixels(2)
        };

        var labeling = new SimpleLabeling(labelAttribute: "electorateName");
        map.SpatialReferenceSystem = SpatialReferenceSystem.Wgs84;
        using var sequence = VectorLayer.Open(geojsonFile, Drivers.GeoJson);


        map.Add(sequence, symbolizer, labeling);
        map.Render("a.png", Renderers.Png);
    }

    [Fact]
    public void AddPointUsingNetTopo()
    {
        var geojsonFile = Path.Combine(DataLocations.MapsCuratedPath, @"2022\Electorates\durack.geojson");

        var jsonOption = new JsonSerializerOptions();
        jsonOption.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
        using var readFile = File.OpenRead(geojsonFile);
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(readFile, jsonOption)!;

        featureCollection.Add(
            new Feature(new NetTopologySuite.Geometries.Point(122.2370, -17.9618),
                new AttributesTable(
                    new Dictionary<string, object>
                    {
                        {
                            "sua", "Broome"
                        }
                    })));
        File.Delete("temp.geojson");
        using (var writeStream = File.OpenWrite("temp.geojson"))
        {
            JsonSerializer.Serialize(writeStream, featureCollection, jsonOption);
        }

        RenderGeo("temp.geojson");
    }

    //https://wiki.openstreetmap.org/wiki/Downloading_data
    //https://overpass-api.de/api/map?bbox=112.920,-32.140,129.001,-13.740
    [Fact]
    public void OpenStreetMap()
    {
        
        var cellsLicense = new License();
        cellsLicense.SetLicense(File.OpenRead(@"C:\Code\CommitmentsManager\src\CommitmentsManager\Aspose.Total.lic"));
        var path = @"C:\Code\durack.osm";

        File.Delete("OpenStreetMap.png");
        var geojsonFile = Path.Combine(DataLocations.MapsCuratedPath, @"2022\Electorates\durack.geojson");

        using var map = new Map(1024, 1024)
        {
            SpatialReferenceSystem = SpatialReferenceSystem.Wgs84
        };
        //17.9618° S, 122.2370° E

        var symbolizer = new SimpleLine
        {
            Width = Measurement.Pixels(2)
        };

        //var labeling = new SimpleLabeling(labelAttribute: "electorateName");
        //using var sequence = VectorLayer.Open(geojsonFile, Drivers.GeoJson);
        //map.Add(sequence, symbolizer, labeling);
        using var osmsequence = VectorLayer.Open(path, Drivers.OsmXml);
        map.Add(osmsequence);
        map.Render("OpenStreetMap.png", Renderers.Png);
    }

    public void RenderGeo(string file)
    {
        File.Delete("a.png");
        using var map = new Map(1024, 1024);

        var symbolizer = new SimpleLine
        {
            Width = Measurement.Pixels(2)
        };

        var labeling = new SimpleLabeling(labelAttribute: "sua");
        map.SpatialReferenceSystem = SpatialReferenceSystem.Wgs84;
        using var sequence = VectorLayer.Open(file, Drivers.GeoJson);

        map.Add(sequence, symbolizer, labeling);
        map.Render("a.png", Renderers.Png);
    }
}