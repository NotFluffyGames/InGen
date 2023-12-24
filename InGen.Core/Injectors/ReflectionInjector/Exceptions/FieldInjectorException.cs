using System;

namespace InGen.Injector.Exceptions
{
    public sealed class FieldInjectorException : Exception
    {
        public FieldInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}