using BenchmarkDotNet.Configs;

namespace BenchmarkApp
{
    public class ConfigDefault : ManualConfig
    {
        public ConfigDefault()
        {
            WithOption(ConfigOptions.DisableOptimizationsValidator, true);
        }
    }
}
