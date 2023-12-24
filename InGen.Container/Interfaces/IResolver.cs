namespace InGen.Container;

public interface IResolver<out T>
{
    T Resolve(object? id = null);
}