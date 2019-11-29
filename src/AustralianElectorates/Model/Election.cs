using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AustralianElectorates
{
    public interface IElection
    {
        int Parliament { get; }
        int Year { get; }
        DateTime Date { get; }
        IReadOnlyList<IElectorate> Electorates { get; }
    }

    [DebuggerDisplay("Parliament={Parliament}, Year={Year}")]
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