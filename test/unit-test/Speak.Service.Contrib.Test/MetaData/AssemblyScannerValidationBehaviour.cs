using System.ComponentModel.DataAnnotations;
using System.Linq;

using Moq;
using Should;
using Xunit;

using Speak.Service.Contrib.MetaData;
using Speak.Service.Core;
using Speak.Service.Core.Diagnostics;
using Speak.Service.Core.MetaData;

namespace Speak.Service.Contrib.Test.MetaData
{
    public class AssemblyScannerValidationBehaviour
    {
        private readonly Mock<ILogger> _logger;
        private readonly IValidationMetaDataProvider _sut;
        private readonly Mock<ITypeProvider> _typeProvider;

        public AssemblyScannerValidationBehaviour()
        {
            _logger = new Mock<ILogger>();
            _typeProvider = new Mock<ITypeProvider>();

            _typeProvider.SetupGet(x => x.Types).Returns(new[] { typeof(RequiredFieldMetaData) }); 
            
            _sut = new AssemblyScannerValidationMetaDataProvider(_typeProvider.Object, _logger.Object);
        }

        [Fact]
        public void Should_describe_validation_attributes()
        {
            var inputAttribute = new RequiredAttribute();
            var results = _sut.GetDataFor(inputAttribute);

            results.ShouldNotBeNull();
        }

        [Fact]
        public void Calls_contained_handler_for_allowed_validation_attribute_type()
        {
            var inputAttribute = new RequiredAttribute();
            var results = _sut.GetDataFor(inputAttribute);

            results.Any(x => (string) x.Value == "required").ShouldBeTrue();
        }

        [Fact]
        public void Returns_null_if_no_handler_is_configured_for_validation_attribute_type()
        {
            var inputAttribute = new StringLengthAttribute(50);
            var results = _sut.GetDataFor(inputAttribute);

            results.ShouldBeNull();
        }

        [Fact]
        public void Logs_missing_validation_attribute_type_handlers()
        {
            var inputAttribute = new StringLengthAttribute(50);
            _sut.GetDataFor(inputAttribute);

            _logger.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }
    }
}