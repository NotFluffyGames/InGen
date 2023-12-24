using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace InGen.Injector;

public class ArrayPool<T>
{
    public static readonly ArrayPool<T> Shared = new();

    private readonly Dictionary<int, ConcurrentBag<object>> _pool = new();
        
    public T[] Rent(int length)
    {
        if (_pool.TryGetValue(length, out var bag))
        {
            if(bag.TryTake(out var a))
                return (T[])a;
        }
        else
        {
            _pool[length] = new();
        }

        return new T[length];
    }

    public void Return(T[] array)
    {
        Array.Clear(array, 0, array.Length);

        if (_pool.TryGetValue(array.Length, out var bag))
            bag.Add(array);
        else
            _pool[array.Length] = new() {array};
    }
}