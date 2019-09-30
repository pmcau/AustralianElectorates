using System;
using System.Collections.Generic;

namespace AustralianElectorates
{
    public interface IElection
    {
        int Parliament { get; }
        int Year { get; }
        DateTime Date { get; }
        IReadOnlyList<IElectorate> Electorates { get; }
    }

    class Election :
        IElection
    {
        public int Parliament { get; set; }
        public int Year { get; set; }
        public DateTime Date { get; set; }
        public IReadOnlyList<IElectorate> Electorates { get; set; } = null!;
        public override string ToString()
        {
            return $"{Parliament} ({Year})";
        }
    }
}