using System;
using System.Diagnostics;

namespace InGen.Container;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class)]
public class InGenContainerAttribute : Attribute
{
    public InGenContainerAttribute(bool multiThreaded = false)
    {
        MultiThreaded = multiThreaded;
    }

    public bool MultiThreaded { get; }
}