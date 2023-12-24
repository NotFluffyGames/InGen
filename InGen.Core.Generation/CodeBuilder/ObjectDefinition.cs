using System.Collections.Generic;
using System.Linq;
using System.Text;
using VContainer.SourceGenerator.CodeBuilder;

namespace CodeGenCore;

public class ObjectGenParams
{
    public static ObjectGenParams ClassDefault => new();
    public static ObjectGenParams StructDefault => new();

    public AccessibilityLevel AccessibilityLevel { get; set; } = AccessibilityLevel.Public;
    public bool IsStatic { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsPartial { get; set; }
    public List<string> Interfaces { get; } = new();
    public List<string> GenericTypes { get; } = new();

    public ObjectGenParams WithInterface(string interfaceName)
    {
        Interfaces.Add(interfaceName);
        return this;
    }

    public ObjectGenParams WithGenericType(string genericTypeName)
    {
        GenericTypes.Add(genericTypeName);
        return this;
    }

    public ObjectGenParams AndPartial()
    {
        IsPartial = true;
        return this;
    }

    public ObjectGenParams AndStatic()
    {
        IsStatic = true;
        return this;
    }

    public ObjectGenParams AndReadOnly()
    {
        IsReadOnly = true;
        return this;
    }

    public string CreateStructStartLine(string name)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(string.Join(" ", StartLineParts().Where(s => !string.IsNullOrWhiteSpace(s))));

        AppendGenerics(stringBuilder);
        AppendInterfaces(stringBuilder);

        return stringBuilder.ToString();

        IEnumerable<string> StartLineParts()
        {
            yield return AccessibilityLevel.GetAccessibilityLevelName();
            yield return IsReadOnly ? "readonly" : string.Empty;
            yield return IsPartial ? "partial" : string.Empty;
            yield return "struct";
            yield return name;
        }
    }

    public string CreateClassStartLine(string name)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(string.Join(" ", StartLineParts().Where(s => !string.IsNullOrWhiteSpace(s))));

        AppendGenerics(stringBuilder);
        AppendInterfaces(stringBuilder);

        return stringBuilder.ToString();

        IEnumerable<string> StartLineParts()
        {
            yield return AccessibilityLevel.GetAccessibilityLevelName();
            yield return IsStatic ? "static" : string.Empty;
            yield return IsPartial ? "partial" : string.Empty;
            yield return "class";
            yield return name;
        }
    }

    private void AppendGenerics(StringBuilder stringBuilder)
    {
        if (GenericTypes.Count > 0)
        {
            stringBuilder.Append($"<{string.Join(",", GenericTypes)}>");
        }
    }

    private void AppendInterfaces(StringBuilder stringBuilder)
    {
        if (Interfaces.Count > 0)
        {
            stringBuilder.Append($" : {string.Join(",", Interfaces)}");
        }
    }
}