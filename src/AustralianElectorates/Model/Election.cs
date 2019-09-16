using System;
using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Election
    {
        internal Election()
        {
        }

        public int Parliament { get; internal set; }
        public int Year { get; internal set; }
        public DateTime Date { get; internal set; }
        public IReadOnlyList<Electorate> Electorates { get; internal set; } = null!;
    }
}