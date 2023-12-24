using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VContainer.SourceGenerator.CodeBuilder;

namespace CodeGenCore;

public static class CodeBuilderExtensions
{
        
    public static string GetAccessibilityLevelName(this AccessibilityLevel accessibilityLevel)
    {
        return accessibilityLevel switch
        {
            AccessibilityLevel.Public => "public",
            AccessibilityLevel.Internal => "internal",
            AccessibilityLevel.Protected => "protected",
            AccessibilityLevel.Private => "private",
            AccessibilityLevel.ProtectedInternal => "protected internal",
            AccessibilityLevel.PrivateProtected => "private protected",
            AccessibilityLevel.Explicit => string.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(accessibilityLevel), accessibilityLevel, null)
        };
    }
        
    public static IScope StartNamespaceScope(this IScope scope, string namespaceName)
    {
        return scope.StartScope($"namespace {namespaceName}");
    }

    public static IScope StartClassScope(this IScope scope, string className, ObjectGenParams? genParams = null)
    {
        genParams ??= ObjectGenParams.ClassDefault;
        return scope.StartScope(genParams.CreateClassStartLine(className));
    }

    public static IScope StartStructScope(this IScope scope, string structName, ObjectGenParams? genParams = null)
    {
        genParams ??= ObjectGenParams.StructDefault;
        return scope.StartScope(genParams.CreateStructStartLine(structName));
    }

    public static IScope StartMethodScope(this IScope scope, MethodGenParams genParams, string returnType, string methodName, params (string paramType, string paramName)[] parameters)
    {
        return scope.StartScope(genParams.CreateStartLine(returnType, methodName, parameters));
    }
    public static IScope StartMethodScope(this IScope scope, AccessibilityLevel accessibilityLevel, string returnType, string methodName, params (string paramType, string paramName)[] parameters)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(string.Join(" ", accessibilityLevel.GetAccessibilityLevelName(), returnType, methodName));
        stringBuilder.Append($"({string.Join(", ", parameters.Select(type => $"{type.paramType} {type.paramName}"))})");
        return scope.StartScope(stringBuilder.ToString());
    }

    public static IScope AddField<T>(this IScope scope, AccessibilityLevel accessibilityLevel, string name)
    {
        return scope.AddField(accessibilityLevel, typeof(T), name);
    }

    public static IScope AddField(this IScope scope, AccessibilityLevel accessibilityLevel, Type type, string name)
    {
        return scope.AddField(accessibilityLevel, type.FullName!, name);
    }
        
    public static IScope AddField(this IScope scope, AccessibilityLevel accessibilityLevel, string typeName, string name)
    {
        return scope.AddLine($"{accessibilityLevel.GetAccessibilityLevelName()} {typeName} {name};");
    }

    public static IScope AddLines(this IScope scope, IEnumerable<string> lines)
    {
        foreach (string line in lines)
        {
            scope.AddLine(line);
        }

        return scope;
    }

    public static IScope Space(this IScope scope, int size = 1)
    {
        for (int i = 0; i < size; i++)
        {
            scope.AddLine(string.Empty);
        }

        return scope;
    }
}