using BenchmarkDotNet.Running;

BenchmarkSwitcher
    .FromAssembly(typeof(ElectorateLocatorBenchmarks).Assembly)
    .Run(args);
