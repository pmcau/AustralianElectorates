using CsvHelper.Configuration.Attributes;

public class TwoPartyPreferred
{
    public string DivisionNm { get; set; }
    public string StateAb { get; set; }
    [Name("Liberal/National Coalition Votes")]
    public int LibNatVotes { get; set; }
    [Name("Liberal/National Coalition Percentage")]
    public string LibNatPercent { get; set; }
    [Name("Australian Labor Party Votes")]
    public int LaborVotes { get; set; }
    [Name("Australian Labor Party Percentage")]
    public string LaborPercent { get; set; }
    public int TotalVotes { get; set; }
    public decimal Swing { get; set; }
}