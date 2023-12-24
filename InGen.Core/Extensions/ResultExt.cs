using System;

namespace InGen.Container;

public static class ResultExt
{
    public static T? AsNullable<T>(this IResult<T> result) where T : struct => result.IsSuccessful() ? result.Value : null;

    public static bool IsSuccessful<T>(this IResult<T> result) => result.Exception == null;

    public static void AssertSuccessful<T>(this IResult<T> result)
    {
        if (result.Exception != null)
            throw result.Exception;
    }

    public static T GetValue<T>(this IResult<T> result)
    {
        result.AssertSuccessful();
        return result.Value!;
    }

    public static bool TryGetValue<T>(this IResult<T> result, out T? value)
    {
        value = result.Value;
        return result.IsSuccessful();
    }

    public static IResult<TOut> Select<TIn, TOut>(this IResult<TIn> result, Func<TIn?, TOut> selector)
        => new Result<TOut>(selector(result.Value), result.Exception);

    public static IResult<object> Box<T>(this IResult<T> result)
        where T : struct
        => result.Select(arg => (object)arg);
}