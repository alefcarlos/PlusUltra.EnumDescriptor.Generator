using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Reflection;

namespace PlusUltra.EnumDescriptor.Generator.Tests
{
    public class GeneratorTest<T> where T : ISourceGenerator, new()
    {
        public T GeneratorInstance {  get; private set; }

        public (GeneratorDriverRunResult Result, ImmutableArray<Diagnostic> Diagnostics) Run(string input)
        {
            Compilation inputCompilation = CreateCompilation(input);
            GeneratorInstance = new T();
            GeneratorDriver driver = CSharpGeneratorDriver.Create(GeneratorInstance);

            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

            GeneratorDriverRunResult runResult = driver.GetRunResult();
            return (runResult, diagnostics);
        }

        private static Compilation CreateCompilation(string source)
            => CSharpCompilation.Create("compilation",
                new[] { CSharpSyntaxTree.ParseText(source) },
                new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));
    }
}
