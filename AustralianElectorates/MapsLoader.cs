using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace AustralianElectorates
{
    public static class MapsLoader
    {
        static ConcurrentDictionary<string, string> cache2016 = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        static ConcurrentDictionary<State, string> cacheStates2016 = new ConcurrentDictionary<State, string>();
        static string australia2016;
        static ConcurrentDictionary<string, string> cacheFuture = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        static ConcurrentDictionary<State, string> cacheStatesFuture= new ConcurrentDictionary<State, string>();
        static string australiaFuture;
        static Assembly assembly;

        static MapsLoader()
        {
            assembly = typeof(MapsLoader).Assembly;
            using (var stream = assembly.GetManifestResourceStream("electorates.json"))
            {
                Electorates = Serializer.Deserialize<List<Electorate>>(stream);
            }
        }

        public static IReadOnlyList<Electorate> Electorates { get; }

        public static IReadOnlyDictionary<string, string> Loaded2016ElectorateMaps => cache2016;
        public static IReadOnlyDictionary<State, string> Loaded2016StateMaps => cacheStates2016;
        public static IReadOnlyDictionary<string, string> LoadedFutureElectorateMaps => cacheFuture;
        public static IReadOnlyDictionary<State, string> LoadedFutureStateMaps => cacheStatesFuture;

        public static string LoadElectorateFutureMap(string electorateName)
        {
            Guard.AgainstNullWhiteSpace(electorateName, nameof(electorateName));
            return cacheFuture.GetOrAdd($@"Future\Electorates\{electorateName}", Inner);
        }
        public static string LoadStateFutureMap(State state)
        {
            return cacheFuture.GetOrAdd($@"Future\{state.ToString().ToLowerInvariant()}", Inner);
        }

        public static string LoadElectorate2016Map(string electorateName)
        {
            Guard.AgainstNullWhiteSpace(electorateName, nameof(electorateName));
            return cache2016.GetOrAdd($@"2016\Electorates\{electorateName}", Inner);
        }

        public static string LoadState2016Map(State state)
        {
            return cache2016.GetOrAdd($@"2016\{state.ToString().ToLowerInvariant()}", Inner);
        }

        public static string LoadAustralia2016Map()
        {
            if (australia2016 == null)
            {
                australia2016 = Inner(@"2016\australia");
            }

            return australia2016;
        }
        public static string LoadAustraliaFutureMap()
        {
            if (australiaFuture == null)
            {
                australiaFuture = Inner(@"Future\australia");
            }

            return australiaFuture;
        }

        static string Inner(string path)
        {
            using (var stream = assembly.GetManifestResourceStream("Maps.zip"))
            using (var archive = new ZipArchive(stream))
            {
                var entry = archive.GetEntry($"{path}.geojson");
                if (entry == null)
                {
                    throw new Exception($"Could not find data for '{path}'.");
                }

                return ReadString(entry);
            }
        }

        public static void LoadAll()
        {
            using (var stream = assembly.GetManifestResourceStream("Maps.zip"))
            using (var archive = new ZipArchive(stream))
            {
                foreach (var entry in archive.Entries)
                {
                    var key = entry.FullName.Split('.').First();
                    var mapString = ReadString(entry);
                    if (key.StartsWith(@"Future\Electorates"))
                    {
                        cacheFuture[key] = mapString;
                        continue;
                    }
                    if (key.StartsWith(@"2016\Electorates"))
                    {
                        cache2016[key] = mapString;
                        continue;
                    }
                    if (key==@"2016\australia")
                    {
                        australia2016 = mapString;
                        continue;
                    }
                    if (key==@"Future\australia")
                    {
                        australiaFuture = mapString;
                        continue;
                    }
                    if (key.StartsWith("Future"))
                    {
                        var state = ParseState(key);
                        cacheStatesFuture[state] = mapString;
                        continue;
                    }
                    if (key.StartsWith("2016"))
                    {
                        var state = ParseState(key);
                        cacheStates2016[state] = mapString;
                        continue;
                    }
                }
            }
        }

        static State ParseState(string key)
        {
            return (State) Enum.Parse(typeof(State), key.Split('\\')[1], true);
        }

        static string ReadString(ZipArchiveEntry entry)
        {
            using (var entryStream = entry.Open())
            using (var reader = new StreamReader(entryStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}