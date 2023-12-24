using System.Collections.Generic;
using System.Linq;
using System.Text;
using VContainer.SourceGenerator.CodeBuilder;

namespace CodeGenCore;

public class MethodGenParams
{
    public static MethodGenParams Default => new();

    public AccessibilityLevel AccessibilityLevel { get; set; } = AccessibilityLevel.Public;
    public bool IsStatic { get; set; }
    public bool IsPartial { get; set; }
    public List<string> GenericTypes { get; } = new();

    public MethodGenParams WithGenericType(string genericTypeName)
    {
        GenericTypes.Add(genericTypeName);
        return this;
    }

    public MethodGenParams AndPartial()
    {
        IsPartial = true;
        return this;
    }

    public MethodGenParams AndStatic()
    {
        IsStatic = true;
        return this;
    }

    public string CreateStartLine(string returnType, string name, IEnumerable<(string paramType, string paramName)> parameters)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(string.Join(" ", StartLineParts().Where(s => !string.IsNullOrWhiteSpace(s))));
        var paramsStr = $"({string.Join(", ", parameters.Select(type => $"{type.paramType} {type.paramName}"))})";
        stringBuilder.Append(paramsStr);

        AppendGenerics(stringBuilder);

        return stringBuilder.ToString();

        IEnumerable<string> StartLineParts()
        {
            yield return AccessibilityLevel.GetAccessibilityLevelName();
            yield return IsStatic ? "static" : string.Empty;
            yield return IsPartial ? "partial" : string.Empty;
            yield return returnType;
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
}