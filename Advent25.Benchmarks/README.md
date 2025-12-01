# Benchmarks
This project contains the benchmarks for my (and other people's) solution.

## Disclaimer

The input is loaded before the benchmarks start, so the initial data input's allocation is not represented in these benchmarks.

Each solution may have had slight adjustments to be compatible with being run multiple times (primarily instances of StreamReader).

The benchmarks are to see how long it takes to run both Part 1 and Part 2. If the solution happens to calculate both part 1 and part 2 in a single pass, then the result from the single pass is used.

Sometimes I'll have additional programs running on my device, so the benchmarks
don't run as fast as they could. However, I leave my device alone while the benchmarks
are running, so they should be consistent and fair within each benchmark.

## Legend

```
Mean      : Arithmetic mean of all measurements
Error     : Half of 99.9% confidence interval
StdDev    : Standard deviation of all measurements
Ratio     : Mean of the ratio distribution ([Current]/[Baseline])
Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
1 ns      : 1 Nanosecond (0.000000001 sec)
1 μs      : 1 Microsecond (0.000001 sec)
```

## Results

All Benchmarks are run as:

BenchmarkDotNet v0.15.8, macOS Tahoe 26.0.1 (25A362) [Darwin 25.0.0]
Apple M3, 1 CPU, 8 logical and 8 physical cores
.NET SDK 10.0.100
[Host]     : .NET 10.0.0 (10.0.0, 10.0.25.52411), Arm64 RyuJIT armv8.0-a
DefaultJob : .NET 10.0.0 (10.0.0, 10.0.25.52411), Arm64 RyuJIT armv8.0-a

### Day 1

| Method |     Mean |    Error |   StdDev | Ratio | Allocated |
|--------|---------:|---------:|---------:|------:|----------:|
| Auros  | 38.13 μs | 0.761 μs | 0.712 μs |  1.00 |         - |