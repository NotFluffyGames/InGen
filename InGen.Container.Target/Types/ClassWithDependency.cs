// <copyright file="ClassWithDependency.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InGen.Target;

public class ClassWithDependency
{
    public IDependentInterface DependentInterface;

    public ClassWithDependency(
        IDependentInterface dependentInterface)
    {
        DependentInterface = dependentInterface;
    }
}