﻿using System;

namespace AustralianElectorates
{
    public class ElectorateNotFoundException : Exception
    {
        public string Name { get; }

        public ElectorateNotFoundException(string name)
        {
            Guard.AgainstWhiteSpace(nameof(name), name);
            Name = name;
        }

        public override string Message => $"Unable to find electorate: '{Name}'.";
    }
}