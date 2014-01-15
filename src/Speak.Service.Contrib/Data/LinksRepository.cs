using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Speak.Service.Contrib.Data
{
    public class LinksRepository : ILinksRepository
    {
        private readonly Database _database;

        public LinksRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<Item> GetLinksFor(Item item, ID templateType)
        {
            var templateItem = _database.Templates[templateType];
            if (templateItem == null)
            {
                return null;
            }

            var links = Sitecore.Globals.LinkDatabase.GetReferrers(item);
            if (links == null)
            {
                return null;
            }

            return links
                    .Where(link => link.SourceDatabaseName == _database.Name)
                    .Select(link => _database.Items[link.SourceItemID])
                    .Where(linkItem => linkItem != null)
                    .ToArray();
        }
    }
}