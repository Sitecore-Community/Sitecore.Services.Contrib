using System.Collections.Generic;
using Sitecore.Services.Core.Model;

namespace Sitecore.Services.Contrib.Model
{
  public class ItemEntity : EntityIdentity
  {
    public List<Field> Fields { get; set; }
  }
}