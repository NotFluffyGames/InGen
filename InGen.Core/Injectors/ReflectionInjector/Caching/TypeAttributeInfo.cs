using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InGen.Caching
{
    public sealed class TypeAttributeInfo
    {
        public readonly IReadOnlyList<MemberInfo> InjectableMembers;
        public readonly IReadOnlyList<FieldInfo> InjectableFields;
        public readonly IReadOnlyList<FieldInfo> OptionalInjectableFields;
        public readonly IReadOnlyList<PropertyInfo> InjectableProperties;
        public readonly IReadOnlyList<PropertyInfo> OptionalInjectableProperties;
        public readonly IReadOnlyList<InjectedMethodInfo> InjectableMethods;
        public readonly IReadOnlyList<InjectedMethodInfo> OptionalInjectableMethods;

        public TypeAttributeInfo(
            IReadOnlyList<FieldInfo> injectableFields, 
            IReadOnlyList<PropertyInfo> injectableProperties, 
            IEnumerable<MethodInfo> injectableMethods, 
            IReadOnlyList<FieldInfo> optionalInjectableFields, 
            IReadOnlyList<PropertyInfo> optionalInjectableProperties, 
            IEnumerable<MethodInfo> optionalInjectableMethods)
        {
            InjectableFields = injectableFields;
            InjectableProperties = injectableProperties;
            InjectableMethods = injectableMethods.Select(mi => new InjectedMethodInfo(mi)).ToArray();
            
            OptionalInjectableFields = optionalInjectableFields;
            OptionalInjectableProperties = optionalInjectableProperties;
            OptionalInjectableMethods = optionalInjectableMethods.Select(mi => new InjectedMethodInfo(mi)).ToArray();
        }
    }
}