using System.ComponentModel.DataAnnotations;
using Sitecore.Services.Core.MetaData;

namespace Sitecore.Services.Contrib.MetaData
{
    public class EmailAddressMetaData : ValidationMetaDataBase<EmailAddressAttribute>
    {
        public EmailAddressMetaData()
          : base("emailAddress")
        {
        }
    }
}