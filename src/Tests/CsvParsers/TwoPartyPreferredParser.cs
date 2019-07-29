using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

static class TwoPartyPreferredParser
{
    public static List<TwoPartyPreferred> Read(string path)
    {
        using (var reader = new StreamReader(path))
        {
            reader.ReadLine();
            using (var csv = new CsvReader(reader))
            {
                return csv.GetRecords<TwoPartyPreferred>().ToList();
            }
        }
    }
}