using System.Reflection;
using Speak.Service.Core.MetaData;
using Speak.Service.Core.Reflection;

namespace Speak.Service.Contrib.MetaData
{
    public class ValidationMetaDataTypeProvider : BaseTypeProvider<IValidationMetaData>
    {
        public ValidationMetaDataTypeProvider(Assembly[] assemblies) : base(assemblies)
        {
        }
    }
}