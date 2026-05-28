using System.Diagnostics;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.Operation.Buffer;
using NetTopologySuite.Simplify;
using Newtonsoft.Json;

public class NtsSmoothingPrototype
{
    // Source is WGS84 lon/lat. 1 degree latitude ≈ 111 km.
    // 0.003° ≈ ~330 m at TAS latitude — small enough to round inlets without
    // swallowing peninsulas, large enough to be visible on a state-level render.
    const double BufferRadiusDegrees = 0.003;
    const int QuadrantSegments = 16;

    [Fact]
    public void Smooth_tas_coastline()
    {
        var sourcePath = Path.Combine(DataLocations.MapsCuratedPath, "2025", "tas.geojson");
        var outDir = Path.Combine(DataLocations.TempPath, "nts-smoothing");
        Directory.CreateDirectory(outDir);

        var smoothedPath = Path.Combine(outDir, "tas_smoothed.geojson");
        var expandedPath = Path.Combine(outDir, "tas_expanded.geojson");

        var serializer = GeoJsonSerializer.Create(new GeometryFactoryEx());

        FeatureCollection featureCollection;
        using (var reader = new JsonTextReader(File.OpenText(sourcePath)))
        {
            featureCollection = serializer.Deserialize<FeatureCollection>(reader)!;
        }

        var bufferParams = new BufferParameters
        {
            QuadrantSegments = QuadrantSegments,
            EndCapStyle = EndCapStyle.Round,
            JoinStyle = JoinStyle.Round
        };

        var sw = Stopwatch.StartNew();
        var smoothed = TransformGeometries(featureCollection, g =>
        {
            // Morphological closing: dilate then erode — rounds sharp inlets,
            // returns roughly to original area. Re-simplify to drop the dense
            // vertices introduced by the buffer arcs.
            var closed = BufferOp.Buffer(g, BufferRadiusDegrees, bufferParams)
                                 .Buffer(-BufferRadiusDegrees, bufferParams);
            return TopologyPreservingSimplifier.Simplify(closed, BufferRadiusDegrees / 4);
        });
        sw.Stop();
        var smoothMs = sw.ElapsedMilliseconds;

        sw.Restart();
        var expanded = TransformGeometries(featureCollection, g =>
            BufferOp.Buffer(g, BufferRadiusDegrees, bufferParams));
        sw.Stop();
        var expandMs = sw.ElapsedMilliseconds;

        Write(serializer, smoothed, smoothedPath);
        Write(serializer, expanded, expandedPath);

        var srcSize = new FileInfo(sourcePath).Length;
        TestContext.Current.AddAttachment("smoothed", smoothedPath);
        TestContext.Current.AddAttachment("expanded", expandedPath);
        Console.WriteLine($"source:    {srcSize,10:N0} bytes");
        Console.WriteLine($"smoothed:  {new FileInfo(smoothedPath).Length,10:N0} bytes  ({smoothMs} ms)");
        Console.WriteLine($"expanded:  {new FileInfo(expandedPath).Length,10:N0} bytes  ({expandMs} ms)");
    }

    static FeatureCollection TransformGeometries(FeatureCollection input, Func<Geometry, Geometry> transform)
    {
        var output = new FeatureCollection();
        foreach (var feature in input)
        {
            output.Add(new Feature(transform(feature.Geometry), feature.Attributes));
        }
        return output;
    }

    static void Write(JsonSerializer serializer, FeatureCollection fc, string path)
    {
        using var stream = File.CreateText(path);
        using var writer = new JsonTextWriter(stream);
        serializer.Serialize(writer, fc);
    }
}
