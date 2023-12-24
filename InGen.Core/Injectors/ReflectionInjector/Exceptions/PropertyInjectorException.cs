using System;

namespace InGen.Injector.Exceptions
{
    public sealed class PropertyInjectorException : Exception
    {
        public PropertyInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}