using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Speak.Service.Core.MetaData;

namespace Speak.Service.Contrib.MetaData
{
    public class EmailAddressMetaData : ValidationMetaDataBase
    {
        public override Dictionary<string, object> Describe(ValidationAttribute attribute)
        {
            var regularExpressionAttribute = (EmailAddressAttribute) attribute;

            // Emits 
            // {
            //          "validatorName": "email",
            //          "errorMessage": "Invalid email address"
            // }

            return new Dictionary<string, object>
                {
                    {ValidatorName, "email"},
                    {ErrorMessage, regularExpressionAttribute.ErrorMessage}
                };
        }

        public override Type ValidationAttributeType
        {
            get { return typeof(EmailAddressAttribute); }
        }
    }
}