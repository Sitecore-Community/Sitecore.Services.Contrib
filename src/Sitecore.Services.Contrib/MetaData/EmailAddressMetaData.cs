using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sitecore.Services.Core.MetaData;

namespace Sitecore.Services.Contrib.MetaData
{
  public class EmailAddressMetaData : ValidationMetaDataBase
  {
    public override Dictionary<string, object> Describe(ValidationAttribute attribute)
    {
      var regularExpressionAttribute = (EmailAddressAttribute)attribute;

      // Emits 
      // {
      //          "validatorName": "emailAddress",
      //          "errorMessage": "Invalid email address"
      // }

      return new Dictionary<string, object>
                {
                    {ValidatorName, "emailAddress"},
                    {ErrorMessage, regularExpressionAttribute.ErrorMessage}
                };
    }

    public override Type ValidationAttributeType
    {
      get { return typeof(EmailAddressAttribute); }
    }
  }
}