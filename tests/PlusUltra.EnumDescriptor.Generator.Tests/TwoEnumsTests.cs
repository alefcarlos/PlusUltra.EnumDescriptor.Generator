using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Xunit;

namespace PlusUltra.EnumDescriptor.Generator.Tests
{
    public class TwoEnumsTests
    {
        private readonly GeneratorTest<EnumDescriptorSourceGenerator> _generator = new();

        [Fact]
        public void Two_Enums_But_One_Decoreated_Should_Generate_Extension()
        {
            // Create the 'input' compilation that the generator will act on
            var input = @"
using System;
namespace MyCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    [GenerateEnumDescriptor]
    public enum Colors {
        Red,
        Green,
        Blue
    }

    public enum Days {
        Monday,
        Sunday
    }
}
";
            var (RunResult, Diagnostics) = _generator.Run(input);

            // We can now assert things about the resulting compilation:
            Debug.Assert(Diagnostics.IsEmpty); // there were no diagnostics created by the generators

            // The runResult contains the combined results of all generators passed to the driver
            Assert.Equal(2, RunResult.GeneratedTrees.Length);
            Assert.True(RunResult.Diagnostics.IsEmpty);

            // Or you can access the individual results on a by-generator basis
            GeneratorRunResult generatorResult = RunResult.Results[0];

            Debug.Assert(generatorResult.Generator == _generator.GeneratorInstance);
            Debug.Assert(generatorResult.Diagnostics.IsEmpty);
            Debug.Assert(generatorResult.GeneratedSources.Length == 2);
            Debug.Assert(generatorResult.Exception is null);

            var attr = generatorResult.GeneratedSources[0];
            var extension = generatorResult.GeneratedSources[1];

            Assert.Equal("GenerateEnumDescriptorAttribute.g.cs", attr.HintName);

            var expected = @"
namespace System
{
    public static class EnumStringExtensions
    {      

        public static string GetDescription(this MyCode.Colors value)
        {
            return value switch
            {
            MyCode.Colors.Red => nameof(MyCode.Colors.Red),
            MyCode.Colors.Green => nameof(MyCode.Colors.Green),
            MyCode.Colors.Blue => nameof(MyCode.Colors.Blue),

                _ => throw new ArgumentException(message: ""Invalid enum value"", paramName: nameof(value))
            };
        }

    }
}";
            Assert.Equal("EnumDescriptionExtensions.g.cs", extension.HintName);
            Assert.Equal(expected, extension.SourceText.ToString());
        }

        [Fact]
        public void Two_Enums_Decoreated_Should_Generate_Extension()
        {
            // Create the 'input' compilation that the generator will act on
            var input = @"
using System;
namespace MyCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    [GenerateEnumDescriptor]
    public enum Colors {
        Red,
        Green,
        Blue
    }

    [GenerateEnumDescriptor]
    public enum Days {
        Monday,
        Sunday
    }
}
";
            var (RunResult, Diagnostics) = _generator.Run(input);

            // We can now assert things about the resulting compilation:
            Debug.Assert(Diagnostics.IsEmpty); // there were no diagnostics created by the generators

            // The runResult contains the combined results of all generators passed to the driver
            Assert.Equal(2, RunResult.GeneratedTrees.Length);
            Assert.True(RunResult.Diagnostics.IsEmpty);

            // Or you can access the individual results on a by-generator basis
            GeneratorRunResult generatorResult = RunResult.Results[0];

            Debug.Assert(generatorResult.Generator == _generator.GeneratorInstance);
            Debug.Assert(generatorResult.Diagnostics.IsEmpty);
            Debug.Assert(generatorResult.GeneratedSources.Length == 2);
            Debug.Assert(generatorResult.Exception is null);

            var attr = generatorResult.GeneratedSources[0];
            var extension = generatorResult.GeneratedSources[1];

            Assert.Equal("GenerateEnumDescriptorAttribute.g.cs", attr.HintName);

            var expected = @"
namespace System
{
    public static class EnumStringExtensions
    {      

        public static string GetDescription(this MyCode.Colors value)
        {
            return value switch
            {
            MyCode.Colors.Red => nameof(MyCode.Colors.Red),
            MyCode.Colors.Green => nameof(MyCode.Colors.Green),
            MyCode.Colors.Blue => nameof(MyCode.Colors.Blue),

                _ => throw new ArgumentException(message: ""Invalid enum value"", paramName: nameof(value))
            };
        }

        public static string GetDescription(this MyCode.Days value)
        {
            return value switch
            {
            MyCode.Days.Monday => nameof(MyCode.Days.Monday),
            MyCode.Days.Sunday => nameof(MyCode.Days.Sunday),

                _ => throw new ArgumentException(message: ""Invalid enum value"", paramName: nameof(value))
            };
        }

    }
}";
            Assert.Equal("EnumDescriptionExtensions.g.cs", extension.HintName);
            Assert.Equal(expected, extension.SourceText.ToString());
        }
    }
}
