using System;
using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Electorate
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
        public double Area { get; set; }
        public string ProductsAndIndustry { get; set; }
        public string NameDerivation { get; set; }
        public DateTime? DateGazetted { get; set; }
        public List<Member> Members { get; set; }
        public string DemographicRating { get; set; }
        public bool ExistIn2016 { get; set; }
        public bool ExistInFuture { get; set; }
    }
}