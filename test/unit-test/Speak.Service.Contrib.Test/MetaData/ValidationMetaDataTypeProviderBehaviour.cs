using System;
using System.Linq;

using Speak.Service.Contrib.MetaData;
using Speak.Service.Core.MetaData;

using Should;
using Xunit;

namespace Speak.Service.Contrib.Test.MetaData
{
    public class ValidationMetaDataTypeProviderBehaviour
    {
        private readonly Type[] _results;

        public ValidationMetaDataTypeProviderBehaviour()
        {
            var sut = new ValidationMetaDataTypeProvider(new[] { typeof(RequiredFieldMetaData).Assembly });
            _results = sut.Types;            
        }

        [Fact]
        public void Returns_types_implementing_IValidateMetaData()
        {
            _results.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public void Contains_expected_type_in_Types()
        {
            _results.ShouldContain(typeof(RequiredFieldMetaData));
        }
    }
}