using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Speak.Service.Contrib
{
    public interface ILinksRepository
    {
        IEnumerable<Item> GetLinksFor(Item id, ID category);
    }
}