using System;
using System.Collections.Generic;
using System.Linq;

using Sitecore.Data;
using Sitecore.Data.Items;

using Speak.Service.Core;

namespace Speak.Service.Contrib.Data
{
    public abstract class SitecoreItemRepository<T> : IRepository<T> where T : EntityIdentity
    {
        private readonly ID _rootItem;
        private readonly ID _templateType;
        private readonly Func<Item, T> _createModelFromItem;

        protected SitecoreItemRepository(ID rootItem, ID templateType, Func<Item, T> createModelFromItem)
        {
            _rootItem = rootItem;
            _templateType = templateType;
            _createModelFromItem = createModelFromItem;
        }

        protected static Database MasterDatabase
        {
            get { return GetDatabase(); }
        }

        public T FindById(Guid id)
        {
            var item = MasterDatabase.GetItem(new ID(id));

            if (item == null) return default(T);

            return _createModelFromItem.Invoke(item);
        }

        public IQueryable<T> GetAll()
        {
            var results = new List<T>();

            var parentItem = MasterDatabase.GetItem(_rootItem);

            if (parentItem != null)
            {
                var items = parentItem.Axes.GetDescendants().Where(x => x.TemplateID == _templateType);
                results.AddRange(items.Select(_createModelFromItem.Invoke));
            }

            return results.AsQueryable();
        }

        public void Add(T entity)
        {
            using (new Sitecore.SecurityModel.SecurityDisabler())  // TODO use a UserSwitcher and switch to the Api User
            {
                var database = MasterDatabase;
                var postsItem = database.GetItem(_rootItem);
                var postTemplate = database.GetTemplate(_templateType);

                if ((postsItem == null) || (postTemplate == null))
                {
                    const string speakBlogNotInstalled = "Speak Blog not installed";
                    throw new InvalidOperationException(speakBlogNotInstalled);
                }

                var newPost = postsItem.Add(GetItemName(entity), postTemplate);

                newPost.Editing.BeginEdit();
                try
                {
                    UpdateFields(entity, newPost);
                }
                finally
                {
                    newPost.Editing.EndEdit();
                }

                // Ensure that the entity contains the identity of the newly created post
                entity.Id = newPost.ID.Guid;
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
            var itemName = GetItemName(entity);

            var currentName = (string) entity.GetType().GetProperty(itemName).GetValue(entity, null);
            if (currentName != null && item.Name == currentName) return;

            item.Name = currentName;
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
            if (entity.Id != Guid.Empty)
            {
                return ((item.TemplateID == _templateType) && (item.ID.Guid == entity.Id));
            }
            
            return ((item.TemplateID == _templateType) &&
                    (item.Name.Equals(GetItemName(entity), StringComparison.InvariantCultureIgnoreCase)));
        }

        private static Database GetDatabase(string databaseName = "master")
        {
            return Sitecore.Configuration.Factory.GetDatabase(databaseName);
        }
    }
}
