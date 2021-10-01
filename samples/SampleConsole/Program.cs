using System;
using System.ComponentModel;
using EnumStringExtensionsNamespace;

namespace SampleConsole
{
    using Alef.Carlos;
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MyEnum.Value1.GetDescription());
            Console.WriteLine(MyEnum.Value2.GetDescription());
            Console.WriteLine(MyEnum.ValueN.GetDescription());

            Console.WriteLine(MyEnumNotOutro.OutroValue.GetDescription());
            Console.WriteLine(MyEnumNotOutro.OutroValue2.GetDescription());
            Console.WriteLine(MyEnumNotOutro.OutroValueN.GetDescription());
        }
    }

    [EnumToString]
    public enum MyEnum
    {
        [Description("Valor 1")]
        Value1,
        Value2,
        [Description("Valor N")]
        ValueN
    }

    public enum MyEnumSemAtributos
    {
        [Description("Valor 1")]
        Value1,
        Value2,
        [Description("Valor N")]
        ValueN
    }
}

namespace Alef.Carlos
{
    [EnumToString]
    public enum MyEnumNotOutro
    {
        [Description("Outro Valor 1")]
        OutroValue,
        OutroValue2,
        OutroValueN
    }
}
