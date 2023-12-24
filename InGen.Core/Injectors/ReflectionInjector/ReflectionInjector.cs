using System;
using System.Collections.Generic;
using System.Reflection;
using InGen.Caching;
using InGen.Injector.Exceptions;

namespace InGen.Injector
{
    public class ReflectionInjector : IInjector
    {
        public void Inject(object toInject, IContainer container)
        {
            var info = ReflectionTypeInfoCache.Get(toInject.GetType());
            Inject(info.InjectableFields, toInject, container);
            Inject(info.InjectableProperties, toInject, container);
            Inject(info.InjectableMethods, toInject, container);
            
            OptionalInject(info.OptionalInjectableFields, toInject, container);
            OptionalInject(info.OptionalInjectableProperties, toInject, container);
            OptionalInject(info.OptionalInjectableMethods, toInject, container);
        }

        private static void OptionalInject(IEnumerable<FieldInfo> optionalInjectableFields, object toInject, IContainer container)
        {
            foreach (var field in optionalInjectableFields) 
                OptionalInject(field, toInject, container);
        }

        private static void OptionalInject(FieldInfo field, object toInject, IContainer container)
        {
            if(container.TryResolve(field.FieldType).TryGetValue(out var result))
                field.SetValue(toInject, result);
        }

        private static void OptionalInject(IEnumerable<PropertyInfo> optionalInjectableProperties, object toInject, IContainer container)
        {
            foreach (var property in optionalInjectableProperties) 
                OptionalInject(property, toInject, container);
        }

        private static void OptionalInject(PropertyInfo property, object toInject, IContainer container)
        {
            if(container.TryResolve(property.PropertyType).TryGetValue(out var result))
                property.SetValue(toInject, result);
        }

        private static void OptionalInject(IEnumerable<InjectedMethodInfo> optionalInjectableMethods, object toInject, IContainer container)
        {
            foreach (var method in optionalInjectableMethods) 
                OptionalInject(method, toInject, container);
        }

        private static void OptionalInject(InjectedMethodInfo method, object toInject, IContainer container)
        {
            var arguments = ArrayPool<object>.Shared.Rent(method.Parameters.Length);
            
            try
            {
                for (var i = 0; i < method.Parameters.Length; i++)
                {
                    if (!container.TryResolve(method.Parameters[i].ParameterType).TryGetValue(out var result))
                        return;
                    else
                        arguments[i] = result!;
                }

                method.MethodInfo.Invoke(toInject, arguments);
            }
            catch (Exception e)
            {
                throw new MethodInjectorException(toInject, method.MethodInfo, e);
            }
            finally
            {
                ArrayPool<object>.Shared.Return(arguments);
            }
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

            try
            {
                for (var i = 0; i < method.Parameters.Length; i++)
                    arguments[i] = container.Resolve(method.Parameters[i].ParameterType);
                
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