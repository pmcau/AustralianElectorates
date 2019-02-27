using System;

namespace AustralianElectorates
{
    public class ElectorateNotFoundException : Exception
    {
        public string Name { get; }

        public ElectorateNotFoundException(string name) :
            base($"Unable to find electorate named '{name}'.")
        {
            Guard.AgainstNullWhiteSpace(nameof(name), name);
            Name = name;
        }
    }
}