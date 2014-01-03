using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Speak.Service.Core;
using Speak.Service.Core.MetaData;

namespace Speak.Service.Contrib.MetaData
{
    public class ValidationMetaDataTypeProvider : ITypeProvider
    {
        private readonly Assembly[] _assemblies;

        public ValidationMetaDataTypeProvider(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public Type[] Types
        {
            get { return ValidationMetaDataTypes(); }
        }

        private Type[] ValidationMetaDataTypes()
        {
            var validators =
                from assembly in _assemblies
                where !assembly.IsDynamic
                from type in GetTypesInAssembly(assembly)
                where !type.IsAbstract
                where !type.IsGenericTypeDefinition
                where typeof(IValidationMetaData).IsAssignableFrom(type)
                select type;

            return validators.ToArray();
        }

        private static IEnumerable<Type> GetTypesInAssembly(Assembly assembly)
        {
            var noTypesFound = new Type[] { };

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return noTypesFound;
            }
        }
    }
}