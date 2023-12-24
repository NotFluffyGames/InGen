using System;

namespace InGen.Container;

[Flags]
public enum ExtraRegistrations
{
    None = 0,
    Self = 1,
    Interfaces = 2
}