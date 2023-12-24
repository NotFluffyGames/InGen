using System;

namespace InGen.Container.Exceptions
{
    internal sealed class PropertyInjectorException : Exception
    {
        public PropertyInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}