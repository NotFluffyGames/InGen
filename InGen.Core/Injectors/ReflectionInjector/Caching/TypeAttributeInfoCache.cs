using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InGen.Caching
{
    public static class ReflectionTypeInfoCache
    {
        private static readonly Dictionary<Type, TypeAttributeInfo> Dictionary = new();
        
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static TypeAttributeInfo Get(Type type)
        {
            if (Dictionary.TryGetValue(type, out var info)) 
                return info;
            
            info = Generate(type);
            Dictionary.Add(type, info);

            return info;
        }
        
        private static TypeAttributeInfo Generate(IReflect type)
        {
            var injectFields = new List<FieldInfo>();
            var optionalInjectFields = new List<FieldInfo>();
                
            foreach (var field in type.GetFields(Flags | BindingFlags.SetField))
            {
                if(field.IsDefined(typeof(InjectAttribute)))
                    injectFields.Add(field);
                else if(field.IsDefined(typeof(OptionalInjectAttribute))) 
                    optionalInjectFields.Add(field);
            }
            
            var injectProperties = new List<PropertyInfo>();
            var optionalInjectProperties = new List<PropertyInfo>();
                
            foreach (var property in type.GetProperties(Flags | BindingFlags.SetProperty))
            {
                if(property.IsDefined(typeof(InjectAttribute)))
                    injectProperties.Add(property);
                else if(property.IsDefined(typeof(OptionalInjectAttribute))) 
                    optionalInjectProperties.Add(property);
            }
            
            var injectMethods = new List<MethodInfo>();
            var optionalInjectMethods = new List<MethodInfo>();
                
            foreach (var method in type.GetMethods(Flags | BindingFlags.InvokeMethod))
            {
                if(method.IsDefined(typeof(InjectAttribute)))
                    injectMethods.Add(method);
                else if(method.IsDefined(typeof(OptionalInjectAttribute))) 
                    optionalInjectMethods.Add(method);
            }

            return new(injectFields, 
                injectProperties, 
                injectMethods, 
                optionalInjectFields, 
                optionalInjectProperties, 
                optionalInjectMethods);
        }
    }
}