using System;

namespace InGen.Container.Exceptions
{
    internal sealed class FieldInjectorException : Exception
    {
        public FieldInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}