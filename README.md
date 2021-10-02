# Enum description generator

This generates a `GetDescription` method for Enum

## Get Started

All enums with `GenerateEnumDescriptorAttribute` will be selected to process 

> You can customize the value using `DescriptionAttribute`

## Example

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
