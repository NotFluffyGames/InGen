// <copyright file="ClassWithDependency.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InGen.Container.Target;

public class ClassWithDependency
{
    private IDependentInterface _dependentInterface;

    public ClassWithDependency(
        IDependentInterface dependentInterface)
    {
        _dependentInterface = dependentInterface;
    }
}