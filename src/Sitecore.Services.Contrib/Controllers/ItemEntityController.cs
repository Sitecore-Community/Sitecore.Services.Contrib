using Sitecore.Services.Contrib.Data;
using Sitecore.Services.Contrib.Model;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace Sitecore.Services.Contrib.Controllers
{
  [ServicesController("custom/item")]
  public class ItemEntityController : EntityService<ItemEntity>
  {
    public ItemEntityController(IRepository<ItemEntity> repository)
      : base(repository)
    {
    }

    public ItemEntityController()
      : this(new ReadOnlyFieldRepository())
    {
    }
  }
}