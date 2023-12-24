using System;
using System.Collections.Generic;

namespace InGen.Container.Generator;

public class Tracker<T>
{
    private readonly Stack<Func<T>> _stack = new();

    public Tracker(Func<T> defaultValue)
    {
        _stack.Push(defaultValue);
    }

    public T Value => _stack.Peek()();

    public IDisposable Push(Func<T> value) => new TrackSubscribe(_stack, value);
    
    private readonly struct TrackSubscribe : IDisposable
    {
        private readonly Stack<Func<T>> _stack;

        public TrackSubscribe(Stack<Func<T>> stack, Func<T> location)
        {
            _stack = stack;
            _stack.Push(location);
        }

        public void Dispose()
        {
            _stack.Pop();
        }
    }
}