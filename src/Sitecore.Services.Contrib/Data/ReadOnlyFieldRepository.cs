using System;
using System.Linq;

using Sitecore.Data;
using Sitecore.Services.Contrib.Model;
using Sitecore.Services.Core;

namespace Sitecore.Services.Contrib.Data
{
  public class ReadOnlyFieldRepository : IRepository<ItemEntity>
  {
    public IQueryable<ItemEntity> GetAll()
    {
      return new ItemEntity[] {}.AsQueryable();
    }

    public ItemEntity FindById(string id)
    {
      return GetDatabase().GetItem(new ID(id)).MapToEntity();
    }

    public void Add(ItemEntity entity)
    {
      throw new NotImplementedException();
    }

    public bool Exists(ItemEntity entity)
    {
      throw new NotImplementedException();
    }

    public void Update(ItemEntity entity)
    {
      throw new NotImplementedException();
    }

    public void Delete(ItemEntity entity)
    {
      throw new NotImplementedException();
    }

    private static Database GetDatabase()
    {
        if ((Context.ContentDatabase == null) && (Context.Database == null))
        {
          // Dev. Note: In ItemService exceptions are mapped to HTTP response status codes.
          // The Sitecore.Services.Infrastructure.Services.EntityService<T> has no sense 
          // of Forbidden as a HttpResponseMessage code.

          throw new UnauthorizedAccessException("No content database in the context");
        }
        
        return Context.ContentDatabase ?? Context.Database;
    }
  }
}