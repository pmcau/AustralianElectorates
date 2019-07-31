using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CsvHelper;

static class HouseTcpByCandidateByPollingPlaceParser
{
    public static List<HouseTcpByCandidateByPollingPlace> Read(string path)
    {
        using (var reader = new StreamReader(path))
        {
            reader.ReadLine();
            using (var csv = new CsvReader(reader))
            {
                return csv.GetRecords<HouseTcpByCandidateByPollingPlace>().ToList();
            }
        }
    }
    public static void ReadPaired(string path)
    {
        var list = Read(path);
        foreach (var electorateGroup in list.GroupBy(x => x.DivisionNm))
        {
            var partyOneVotes = 0;
            decimal partyOnePreviousVotes = 0;
            string partyOneName = null;

            var partyTwoVotes = 0;
            string partyTwoName;
            decimal partyTwoPreviousVotes = 0;

            var places = electorateGroup.ToList();
            for (var index = 0; index < places.Count;)
            {
                var partyOne = places[index];
                partyOneName = partyOne.PartyAb;
                partyOneVotes += partyOne.OrdinaryVotes;
                partyOnePreviousVotes += partyOne.OrdinaryVotes * partyOne.Swing;


                var partyTwo = places[index+1];
                partyTwoName = partyTwo.PartyAb;
                partyTwoVotes += partyTwo.OrdinaryVotes;
                partyTwoPreviousVotes += partyTwo.OrdinaryVotes * partyTwo.Swing;

                index = index + 2;
            }

            var oneSwing = (partyOneVotes-partyOnePreviousVotes)/partyOneVotes;
            var twoSwing = (partyTwoVotes-partyTwoPreviousVotes)/partyTwoVotes;
            var oneVotes = partyOneVotes+partyTwoVotes;
            Debug.WriteLine(partyOneName);

        }
    }
}