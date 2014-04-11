using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Sitecore.Services.Contrib.MetaData;
using Sitecore.Services.Core.MetaData;

using Should;
using Xunit;

namespace Sitecore.Services.Contrib.Test.MetaData
{
  public class EmailAddressMetaDataBehaviour
  {
    private readonly Dictionary<string, object> _result;
    private readonly IValidationMetaData _sut;
    private const string FieldName = "MyProperty";

    public EmailAddressMetaDataBehaviour()
    {
      var attribute = new EmailAddressAttribute { ErrorMessage = "My message" };

      _sut = new EmailAddressMetaData();
      _result = _sut.Describe(attribute, FieldName);
    }

    [Fact]
    public void MetaData_provided_for_EmailAddressAttribute()
    {
      _sut.ValidationAttributeType.ShouldEqual(typeof(EmailAddressAttribute));
    }

    [Fact]
    public void Returns_email_as_validatorName()
    {
      _result["validatorName"].ShouldEqual("emailAddress");
    }

    [Fact]
    public void Returns_expected_email_error_message()
    {
      _result["errorMessage"].ShouldEqual("My message");
    }

    [Fact]
    public void Returns_no_params()
    {
      _result.ContainsKey("param").ShouldBeFalse();
    }

    [Fact]
    public void Returns_default_error_message_when_not_specified()
    {
      var attribute = new EmailAddressAttribute();
      var metaData = new EmailAddressMetaData();

      var result = metaData.Describe(attribute, FieldName);

      result["errorMessage"].ShouldEqual(string.Format("The {0} field is not a valid e-mail address.", FieldName));
    }
  }
}