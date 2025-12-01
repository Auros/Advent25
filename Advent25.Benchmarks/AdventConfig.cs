using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;

namespace Advent25.Benchmarks;

public class AdventConfig : ManualConfig
{
    public AdventConfig()
    {
        AddColumn(TargetMethodColumn.Method);
        
        AddColumn(StatisticColumn.Mean);
        AddColumn(StatisticColumn.Error);
        AddColumn(StatisticColumn.StdDev);
        AddColumn(BaselineRatioColumn.RatioMean);

        AddDiagnoser(MemoryDiagnoser.Default);
        AddColumnProvider(DefaultColumnProviders.Metrics);

        // AddJob(Job.ShortRun);
        AddLogger(ConsoleLogger.Unicode);
        AddExporter(MarkdownExporter.GitHub);
    }
}