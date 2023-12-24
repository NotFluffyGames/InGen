using System;

namespace InGen;

[Flags]
public enum ExtraRegistrations
{
    None = 0,
    Self = 1,
    Interfaces = 2
}