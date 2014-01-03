using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Speak.Service.Core;
using Speak.Service.Core.Diagnostics;
using Speak.Service.Core.MetaData;

namespace Speak.Service.Contrib.MetaData
{
    public interface IValidationTypeProvider
    {
        IEnumerable<IValidationMetaData> ValidationTypes { get; }
    }

    public class AssemblyScannerValidationMetaDataProvider : IValidationMetaDataProvider
    {
        private readonly ITypeProvider _typeProvider;
        private readonly ILogger _logger;

        private static IList<IValidationMetaData> _validatorHandlers;

        public AssemblyScannerValidationMetaDataProvider(ITypeProvider typeProvider, ILogger logger)
        {
            _typeProvider = typeProvider;
            _logger = logger;
        }

        public Dictionary<string, object> GetDataFor(ValidationAttribute attribute)
        {
            var attributreType = attribute.GetType();

            var handler = ValidatorHandlers.SingleOrDefault(x => x.ValidationAttributeType == attributreType);
            if (handler == null)
            {
                _logger.Warn("Missing validation handler ({0})", attributreType.FullName);

                return null;
            }

            return handler.Describe(attribute);
        }

        protected IEnumerable<IValidationMetaData> ValidatorHandlers
        {
            get
            {
                if (_validatorHandlers == null)
                {
                    _validatorHandlers = new List<IValidationMetaData>();

                    foreach (var instance in _typeProvider.Types.Select(Activator.CreateInstance))
                    {
                        _logger.Info("Loaded validation metadata handler ({0})", instance.GetType().Name);
                        _validatorHandlers.Add((IValidationMetaData)instance);
                    }
                }

                return _validatorHandlers;
            }
        }
    }
}
