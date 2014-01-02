using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Moq;
using Should;
using Speak.Service.Contrib.MetaData;
using Speak.Service.Core.Diagnostics;
using Speak.Service.Core.MetaData;
using Xunit;

namespace Speak.Service.Contrib.Test.MetaData
{
    public class AssemblyScannerValidationBehaviour
    {
        private readonly Mock<IValidationMetaData> _internalValidator;
        private readonly Mock<ILogger> _logger;
        private readonly IValidationMetaData _validator;

        public AssemblyScannerValidationBehaviour()
        {
            _internalValidator = new Mock<IValidationMetaData>();
            _logger = new Mock<ILogger>();

            _internalValidator
                .Setup(x => x.Describe(It.IsAny<RequiredAttribute>()))
                .Returns(new Dictionary<string, object>());

            var handlers = new Dictionary<Type, IValidationMetaData>
                {
                    { typeof (RequiredAttribute), _internalValidator.Object }
                };

            _validator = new AssemblyScannerValidationMetaDataProvider();
        }

        [Fact]
        public void Should_describe_validation_attributes()
        {
            var inputAttribute = new RequiredAttribute();
            var results = _validator.Describe(inputAttribute);

            results.ShouldNotBeNull();
        }

//        [Fact]
//        public void Calls_contained_handler_for_allowed_validation_attribute_type()
//        {
//            var inputAttribute = new RequiredAttribute();
//            _validator.Describe(inputAttribute);
//
//            _internalValidator.Verify(x => x.Describe(It.IsAny<RequiredAttribute>()), Times.Once);
//        }
//
//        [Fact]
//        public void Returns_null_if_no_handler_is_configured_for_validation_attribute_type()
//        {
//            var inputAttribute = new StringLengthAttribute(50);
//            var results = _validator.Describe(inputAttribute);
//
//            results.ShouldBeNull();
//        }
//
//        [Fact]
//        public void Logs_missing_validation_attribute_type_handlers()
//        {
//            var inputAttribute = new StringLengthAttribute(50);
//            _validator.Describe(inputAttribute);
//
//            _logger.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
//        }
    }
}