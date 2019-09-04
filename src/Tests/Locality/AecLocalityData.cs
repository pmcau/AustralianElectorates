public class AecLocalityData
{
    public AecLocalityData(string place, int postcode, string electorate)
    {
        Place = place;
        Postcode = postcode;
        Electorate = electorate;
    }

    public string Place { get; }
    public int Postcode { get; }
    public string Electorate { get; }
}