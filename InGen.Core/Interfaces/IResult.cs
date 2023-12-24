using System;

namespace InGen;

public interface IResult<out T>
{
    T? Value { get; }
    Exception? Exception { get; }
}