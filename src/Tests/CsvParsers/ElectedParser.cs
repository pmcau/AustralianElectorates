using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

static class ElectedParser
{
    public static List<Elected> Read(string path)
    {
        using (var reader = new StreamReader(path))
        {
            reader.ReadLine();
            using (var csv = new CsvReader(reader))
            {
                return csv.GetRecords<Elected>().ToList();
            }
        }
    }
}