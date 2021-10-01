# Enum to String Source Generator

The source generator generates a `ToDescription` method for some enumerations

## Get Started

All enums with `EnumToStringAttribute` will be selected to process 

> You can customiza the value using `DescriptionAttribute`

## Example

```csharp
[EnumToString]
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
namespace EnumStringExtensionsNamespace
{
    public static class EnumStringExtensions
    {      

        public static string ToDescription(this Namespace.MyEnum value)
        {
            return value switch
            {
                SampleConsole.MyEnum.Value1 => "Custom 1",
                SampleConsole.MyEnum.Value2 => nameof(SampleConsole.MyEnum.Value2),
                SampleConsole.MyEnum.ValueN => "Custom Value N",
                _ => value.ToString()
            };
        }
    }
}
```