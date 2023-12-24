using System;

namespace InGen;

public readonly struct Result<T>
    : IResult<T>
{
    public static Result<T> FromValue(T val) => new(value: val, exception: null);

    public static Result<T> FromException(Exception ex) => new(value: default, exception: ex);

    public T? Value { get; }
    public Exception? Exception { get; }

    public Result(T? value, Exception? exception)
    {
        Value = value;
        Exception = exception;
    }

    public static implicit operator Result<T>((T, Exception) tuple) => new(tuple.Item1, tuple.Item2);
    public static implicit operator Result<T>((Exception, T) tuple) => new(tuple.Item2, tuple.Item1);
    public static implicit operator Result<T>(Tuple<T, Exception> tuple) => new(tuple.Item1, tuple.Item2);
    public static implicit operator Result<T>(Tuple<Exception, T> tuple) => new(tuple.Item2, tuple.Item1);

    public static explicit operator (T?, Exception?)(Result<T> result) => (result.Value, result.Exception);
    public static explicit operator (Exception?, T?)(Result<T> result) => (result.Exception, result.Value);
    public static explicit operator Tuple<T?, Exception?>(Result<T> result) => new(result.Value, result.Exception);
    public static explicit operator Tuple<Exception?, T?>(Result<T> result) => new(result.Exception, result.Value);

    public static implicit operator Result<T>(Exception exception) => FromException(exception);
    public static implicit operator Result<T>(T value) => FromValue(value);
    public static explicit operator T(Result<T> result) => result.GetValue();

    public override string? ToString()
    {
        return this.IsSuccessful()
            ? Value?.ToString()
            : Exception?.ToString();
    }
}

public static class Result
{
    public static Result<T> FromValue<T>(T value) => Result<T>.FromValue(value);
    public static Result<T> FromException<T>(Exception exception) => Result<T>.FromException(exception);
}