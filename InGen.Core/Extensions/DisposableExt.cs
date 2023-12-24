using System;
using System.Collections.Generic;

namespace CodeGenCore.Extensions;

public static class DisposableExt
{
    public static T AddTo<T>(this T disposable, ICollection<IDisposable> collection)
        where T : IDisposable
    {
        collection.Add(disposable);
        return disposable;
    }

    public static void ClearAndDispose<T>(this ICollection<T> disposables)
        where T : IDisposable
    {
        foreach (var disposable in disposables) 
            disposable?.Dispose();
        
        disposables.Clear();
    }
}