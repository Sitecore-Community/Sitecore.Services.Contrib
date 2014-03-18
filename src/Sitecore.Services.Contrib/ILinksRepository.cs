using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Services.Contrib
{
  public interface ILinksRepository
  {
    IEnumerable<Item> GetLinksFor(Item id, ID category);
  }
}