using BenchmarkDotNet.Attributes;
using System;

namespace BenchmarkApp
{
    [MemoryDiagnoser]
    [Config(typeof(ConfigDefault))]
    public class EnumBench
    {
        [GenerateEnumDescriptor]
        public enum Colors
        {
            Red,
            Green,
            Blue
        }
            

        [Benchmark(Baseline = true)]
        public string EnumToString()
        {
            return Colors.Red.ToString();
        }

        [Benchmark]
        public string EnumGetDescription()
        {
            return Colors.Red.GetDescription();
        }
    }
}
