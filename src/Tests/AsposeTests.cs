using Aspose.Gis;
using Aspose.Gis.Geometries;
using Aspose.Gis.Rendering;
using Aspose.Gis.Rendering.Labelings;
using Aspose.Gis.Rendering.Symbolizers;
using Aspose.Gis.SpatialReferencing;
using AustralianElectorates;

[UsesVerify]
public class AsposeTests
{
    [Fact]
    public void AddLabel()
    {
        File.Delete("a.png");
        var geojsonFile = Path.Combine(DataLocations.MapsCuratedPath,@"2022\Electorates\durack.geojson");
        using var map = new Map(1024,1024);
        //17.9618° S, 122.2370° E

        var symbolizer = new SimpleLine
            {Width = Measurement.Pixels(2)};

        var labeling = new SimpleLabeling(labelAttribute: "electorateName");
        map.SpatialReferenceSystem = SpatialReferenceSystem.Wgs84;
        using var sequence = VectorLayer.Open(geojsonFile, Drivers.GeoJson);
        
        
        MultiPoint multipoint = new MultiPoint();
        multipoint.Add(new Point(1, 2));
        multipoint.Add(new Point(3, 4));
        map.Add(sequence, symbolizer, labeling);
        map.Render("a.png", Renderers.Png);
    }
}