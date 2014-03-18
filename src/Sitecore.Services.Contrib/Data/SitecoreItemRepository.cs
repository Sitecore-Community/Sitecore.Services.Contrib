using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Model;

namespace Sitecore.Services.Contrib.Data
{
  public abstract class SitecoreItemRepository<T> : IRepository<T> where T : EntityIdentity
  {
    private readonly ID _rootItem;
    private readonly ID _templateType;

    protected SitecoreItemRepository(ID rootItem, ID templateType)
    {
      _rootItem = rootItem;
      _templateType = templateType;
    }

    protected static Database MasterDatabase
    {
      get { return GetDatabase(); }
    }

    public T FindById(string id)
    {
      var item = MasterDatabase.GetItem(new ID(id));

      if (item == null) return default(T);

      return CreateModelFrom(item);
    }

    protected abstract T CreateModelFrom(Item item);

    public IQueryable<T> GetAll()
    {
      var results = new List<T>();

      var parentItem = MasterDatabase.GetItem(_rootItem);

      if (parentItem != null)
      {
        var items = parentItem.Axes.GetDescendants().Where(x => x.TemplateID == _templateType);
        results.AddRange(items.Select(CreateModelFrom));
      }

      return results.AsQueryable();
    }

    public void Add(T entity)
    {
      using (new Sitecore.SecurityModel.SecurityDisabler())  // TODO use a UserSwitcher and switch to the Api User
      {
        var database = MasterDatabase;
        var rootItem = database.GetItem(_rootItem);
        var templateItem = database.GetTemplate(_templateType);

        if (rootItem == null)
        {
          throw new InvalidOperationException(string.Format("Root Item ({0}) not found", _rootItem));
        }

        if (templateItem == null)
        {
          throw new InvalidOperationException(string.Format("Template type ({0}) not found", _templateType));
        }

        var newItem = rootItem.Add(GetItemName(entity), templateItem);

        newItem.Editing.BeginEdit();
        try
        {
          UpdateFields(entity, newItem);
        }
        finally
        {
          newItem.Editing.EndEdit();
        }

        // Ensure that the entity contains the identity of the newly created item
        entity.Id = newItem.ID.Guid.ToString();
      }
    }

    public void Update(T entity)
    {
      using (new Sitecore.SecurityModel.SecurityDisabler())  // TODO use a UserSwitcher and switch to the Api User
      {
        var database = MasterDatabase;
        var itemToUpdate = database.GetItem(new ID(entity.Id));

        if (itemToUpdate == null)
        {
          throw new InvalidOperationException(string.Format("Item ({0}) could not be found", entity.Id));
        }

        itemToUpdate.Editing.BeginEdit();
        try
        {
          UpdateItemName(entity, itemToUpdate);

          UpdateFields(entity, itemToUpdate);
        }
        finally
        {
          itemToUpdate.Editing.EndEdit();
        }
      }
    }

    private void UpdateItemName(T entity, Item item)
    {
      var entityName = GetItemName(entity);

      if (!string.IsNullOrEmpty(entityName) && item.Name == entityName) return;

      item.Name = entityName;
    }

    protected abstract string GetItemName(T entity);

    protected virtual void UpdateFields(T entity, Item item)
    {
      // Default implementation updates no fields
    }

    public bool Exists(T entity)
    {
      var parentItem = MasterDatabase.GetItem(_rootItem);

      if (parentItem != null)
      {
        return parentItem.Axes.GetDescendants().Any(x => IsMatch(entity, x));
      }

      return false;
    }

    public void Delete(T entity)
    {
      using (new Sitecore.SecurityModel.SecurityDisabler()) // TODO use a UserSwitcher and switch to the Api User
      {
        var item = MasterDatabase.GetItem(new ID(entity.Id));
        item.Delete();
      }
    }

    protected virtual bool IsMatch(T entity, Item item)
    {
      // Entity.Id can be unset when creating entities, if so fallback to comparison by name 
      if (entity.HasIdentity())
      {
        return ((item.TemplateID == _templateType) &&
                (item.ID.Guid.ToString().Equals(entity.Id, StringComparison.InvariantCultureIgnoreCase)));
      }

      return ((item.TemplateID == _templateType) &&
              (item.Name.Equals(GetItemName(entity), StringComparison.InvariantCultureIgnoreCase)));
    }

    private static Database GetDatabase(string databaseName = "master")
    {
      return Sitecore.Configuration.Factory.GetDatabase(databaseName);
    }

    protected void UpdateItemListField(Field field, ICollection<Guid> itemIDs)
    {
      MultilistField multilistField = field;
      if (multilistField == null) return;

      if (itemIDs == null)
      {
        foreach (var id in multilistField.TargetIDs)
        {
          multilistField.Remove(id.ToString());
        }
      }
      else
      {
        foreach (var id in multilistField.TargetIDs.Where(id => !itemIDs.Contains(id.Guid)))
        {
          multilistField.Remove(id.ToString());
        }

        foreach (var id in itemIDs.Select(x => new ID(x)).Where(id => !multilistField.TargetIDs.Contains(id)))
        {
          multilistField.Add(id.ToString());
        }
      }
    }
  }
}