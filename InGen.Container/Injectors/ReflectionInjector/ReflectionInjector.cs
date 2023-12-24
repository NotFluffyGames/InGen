using System;
using System.Collections.Generic;
using System.Reflection;
using InGen.Container.Caching;
using InGen.Container.Exceptions;

namespace InGen.Container.Injectors
{
    public class ReflectionInjector : IInjector
    {
        public void Inject(object toInject, IContainer container)
        {
            var info = ReflectionTypeInfoCache.Get(toInject.GetType());
            Inject(info.InjectableFields, toInject, container);
            Inject(info.InjectableProperties, toInject, container);
            Inject(info.InjectableMethods, toInject, container);
        }

        private static void Inject(IEnumerable<FieldInfo> fields, object instance, IContainer container)
        {
            foreach (var field in fields) 
                Inject(field, instance, container);
        }
        
        private static void Inject(FieldInfo field, object instance, IContainer container)
        {
            try
            {
                field.SetValue(instance, container.Resolve(field.FieldType));
            }
            catch (Exception e)
            {
                throw new FieldInjectorException(e);
            }
        }

        private static void Inject(IEnumerable<PropertyInfo> properties, object instance, IContainer container)
        {
            foreach (var field in properties) 
                Inject(field, instance, container);
        }
        
        private static void Inject(PropertyInfo property, object instance, IContainer container)
        {
            try
            {
                property.SetValue(instance, container.Resolve(property.PropertyType));
            }
            catch (Exception e)
            {
                throw new PropertyInjectorException(e);
            }
        }

        private static void Inject(IEnumerable<InjectedMethodInfo> methods, object instance, IContainer container)
        {
            foreach (var t in methods) 
                Inject(t, instance, container);
        }
        
        private static void Inject(InjectedMethodInfo method, object instance, IContainer container)
        {
            var arguments = ArrayPool<object>.Shared.Rent(method.Parameters.Length);

            for (var i = 0; i < method.Parameters.Length; i++)
                arguments[i] = container.Resolve(method.Parameters[i].ParameterType);

            try
            {
                method.MethodInfo.Invoke(instance, arguments);
            }
            catch (Exception e)
            {
                throw new MethodInjectorException(instance, method.MethodInfo, e);
            }
            finally
            {
                ArrayPool<object>.Shared.Return(arguments);
            }
        }
    }
}