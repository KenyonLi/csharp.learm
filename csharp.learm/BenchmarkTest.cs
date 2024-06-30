using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm;

//[MemoryDiagnoser]
//吞吐量
//[SimpleJob(launchCount: 3, warmupCount: 10, iterationCount: 30)]
//[SimpleJob(RuntimeMoniker.Net48)]
//[SimpleJob(RuntimeMoniker.Net80)]
//[SimpleJob(RuntimeMoniker.Net70)]
//[SimpleJob(RuntimeMoniker.Mono)]
[CsvMeasurementsExporter]
public class BenchmarkTest
{
    [Benchmark]
    public void As()
    {
        var x = new List<int>();
        for (int i = 0; i < 10_000_000; i++)
        {
            x.Add(i);
        }
    }
}
