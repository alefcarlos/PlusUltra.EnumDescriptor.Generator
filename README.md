| |  |
|--|--|
|[![ci](https://github.com/alefcarlos/PlusUltra.EnumDescriptor.Generator/actions/workflows/ci.yml/badge.svg)](https://github.com/alefcarlos/PlusUltra.EnumDescriptor.Generator/actions/workflows/ci.yml)|[![ci](https://img.shields.io/static/v1?label=nuget&message=download&color=brightgreen)](https://www.nuget.org/packages/PlusUltra.EnumDescriptor.Generator/)|

# Enum description generator

This generates a `GetDescription` method for Enum

## Get Started

All enums with `GenerateEnumDescriptorAttribute` will be selected to process 

> You can customize the value using `DescriptionAttribute`

### Installing

`dotnet add package PlusUltra.EnumDescriptor.Generator`

### Decorating enums

```csharp
[GenerateEnumDescriptor]
public enum MyEnum
{
    [Description("Custom 1")]
    Value1,
    Value2,
    [Description("Custom Value N")]
    ValueN
}
```

This example will generate:

```csharp
namespace System
{
    public static class EnumStringExtensions
    {   
        public static string GetDescription(this Namespace.MyEnum value)
        {
            return value switch
            {
                SampleConsole.MyEnum.Value1 => "Custom 1",
                SampleConsole.MyEnum.Value2 => nameof(SampleConsole.MyEnum.Value2),
                SampleConsole.MyEnum.ValueN => "Custom Value N",
                _ => throw new ArgumentException(message: "Invalid enum value", paramName: nameof(value))
            };
        }
    }
}
```

Now you can use `GetDescription` instead of `ToString`;

```csharp
MyEnum value = MyEnum.Value1;
Console.WriteLine(value.GetDescription());
```

### Materials 

 - https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md
 - https://www.infoq.com/articles/CSharp-Source-Generator/
 - https://levelup.gitconnected.com/four-ways-to-generate-code-in-c-including-source-generators-in-net-5-9e6817db425
 - https://github.com/meziantou/Meziantou.Framework/tree/main/src/Meziantou.Framework.FastEnumToStringGenerator