using System;

namespace InGen.Container;

public interface IResult<out T>
{
    T? Value { get; }
    Exception? Exception { get; }
}