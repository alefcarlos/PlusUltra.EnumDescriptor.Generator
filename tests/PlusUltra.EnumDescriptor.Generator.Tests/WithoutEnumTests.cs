using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Xunit;

namespace PlusUltra.EnumDescriptor.Generator.Tests
{
    public class WithoutEnumTests
    {
        private readonly GeneratorTest<EnumDescriptorSourceGenerator> _generator = new();

        [Fact]
        public void Without_Enums_Should_Generate_Only_Attribute()
        {
            // Create the 'input' compilation that the generator will act on
            var input = @"
namespace MyCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}
";

            var (RunResult, Diagnostics) = _generator.Run(input);

            // We can now assert things about the resulting compilation:
            Debug.Assert(Diagnostics.IsEmpty); // there were no diagnostics created by the generators

            // The runResult contains the combined results of all generators passed to the driver
            Assert.Single(RunResult.GeneratedTrees);
            Assert.True(RunResult.Diagnostics.IsEmpty);

            // Or you can access the individual results on a by-generator basis
            GeneratorRunResult generatorResult = RunResult.Results[0];

            Debug.Assert(generatorResult.Generator == _generator.GeneratorInstance);
            Debug.Assert(generatorResult.Diagnostics.IsEmpty);
            Debug.Assert(generatorResult.GeneratedSources.Length == 1);
            Debug.Assert(generatorResult.Exception is null);

            Assert.Equal("GenerateEnumDescriptorAttribute.g.cs", generatorResult.GeneratedSources[0].HintName);
            var expected = @"
[System.Diagnostics.Conditional(""GenerateEnumDescriptor_Attributes"")]
[System.AttributeUsage(System.AttributeTargets.Enum, AllowMultiple = false)]
internal sealed class GenerateEnumDescriptor : System.Attribute
{
}
";
            var source = generatorResult.GeneratedSources[0].SourceText.ToString();
            Assert.Equal(expected, source);
        }
    }
}
