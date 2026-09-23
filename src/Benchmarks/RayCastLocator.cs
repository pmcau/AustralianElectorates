// The lookup from 12.10.0, before edges were indexed by latitude band: a bounding box check per
// electorate, then a ray cast over every edge of every ring. Kept as the baseline for LocateBenchmarks.
class RayCastLocator(List<(IElectorate electorate, (double X, double Y)[][] rings)> items)
{
    Area[] areas = [.. items.Select(_ => new Area(_.electorate, _.rings))];

    public IElectorate? Find(double latitude, double longitude)
    {
        foreach (var area in areas)
        {
            if (area.Contains(longitude, latitude))
            {
                return area.Electorate;
            }
        }

        return null;
    }

    class Area
    {
        public IElectorate Electorate { get; }
        double minX = double.MaxValue;
        double minY = double.MaxValue;
        double maxX = double.MinValue;
        double maxY = double.MinValue;
        (double X, double Y)[][] rings;

        public Area(IElectorate electorate, (double X, double Y)[][] rings)
        {
            Electorate = electorate;
            this.rings = rings;
            foreach (var ring in rings)
            {
                foreach (var (x, y) in ring)
                {
                    minX = Math.Min(minX, x);
                    minY = Math.Min(minY, y);
                    maxX = Math.Max(maxX, x);
                    maxY = Math.Max(maxY, y);
                }
            }
        }

        public bool Contains(double x, double y)
        {
            if (x < minX || x > maxX || y < minY || y > maxY)
            {
                return false;
            }

            var inside = false;
            foreach (var ring in rings)
            {
                for (int i = 0, j = ring.Length - 1; i < ring.Length; j = i++)
                {
                    var (xi, yi) = ring[i];
                    var (xj, yj) = ring[j];
                    if (yi > y != yj > y &&
                        x < (xj - xi) * (y - yi) / (yj - yi) + xi)
                    {
                        inside = !inside;
                    }
                }
            }

            return inside;
        }
    }
}
