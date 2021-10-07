using BenchmarkDotNet.Running;
using System;

namespace BenchmarkApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<EnumBench>();
        }
    }
}
