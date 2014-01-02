using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Speak.Service.Core;

namespace Speak.Service.Contrib.MetaData
{
    public class AssemblyScannerValidationMetaDataProvider : Speak.Service.Core.MetaData.IValidationMetaData
    {
        public Dictionary<string, object> Describe(ValidationAttribute attribute)
        {
            return new Dictionary<string, object>();
        }
    }
}

namespace Speak.Service.Infrastructure.Web.Http
{
    public class ApiControllerTypeProvider : ITypeProvider
    {
        private readonly Assembly[] _assemblies;

        public ApiControllerTypeProvider(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public Type[] Types
        {
            get { return GetApiControllerTypes(); }
        }

        private Type[] GetApiControllerTypes()
        {
            var controllers =
                from assembly in _assemblies
                where !assembly.IsDynamic
                from type in GetTypesInAssembly(assembly)
                where !type.IsAbstract
                where !type.IsGenericTypeDefinition
                where type.IsSubclassOf(typeof(ApiController))
                select type;

            return controllers.ToArray();
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
