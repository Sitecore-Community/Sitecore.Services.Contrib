using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Services.Contrib.Model;

namespace Sitecore.Services.Contrib.Data
{
  public static class ItemExtensions
  {
    public static ItemEntity MapToEntity(this Item item)
    {
      var entity = new ItemEntity
      {
        Fields = item.Fields.Select(x => new Field {Name = x.Name, Value = x.Value}).ToList(), 
        Id = item.ID.Guid.ToString()
      };
      return entity;
    }
  }
}