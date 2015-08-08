using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage
{
    public class Store<T, TKey>
        : IStore<T, TKey>, IDisposable where T : IEntity<TKey>
    {
        private bool _disposed;
        private readonly string filename;

        private static readonly object objLock = new object();

        private readonly IPantherFileSystem fileSystem;
        private List<T> items;

        public Store(IPantherFileSystem fileSystem)
        {
            lock (objLock)
            {
                this.fileSystem = fileSystem;
                filename = CreateFilename();
                ReloadFile();
            }
        }

        public void ReloadFile()
        {
            try
            {
                items = this.fileSystem.ReadFile<List<T>>(filename) ?? new List<T>();
            }
            catch
            {
                items = new List<T>();
                Save();
            }
        }

        public virtual string CreateFilename()
        {
            return fileSystem.CreateFilename(typeof(T));
        }

        public T GetByKey(TKey key)
        {
            return items.FirstOrDefault(x => x.Id.Equals(key));
        }

        public void Add(T entity)
        {
            if (Contains(entity.Id))
                return;
            if(entity.Id.Equals(default(TKey)))
                entity.Id = GenerateKey();

            items.Add(entity);
            Save();
        }

        public virtual TKey GenerateKey()
        {
            return default(TKey);
        }

        public void Update(T entity)
        {
            if (!Contains(entity.Id))
                return;

            var item = GetByKey(entity.Id);
            var index = items.IndexOf(item);
            items[index] = entity;

            Save();
        }

        public void Delete(T entity)
        {
            if (!Contains(entity.Id))
                return;

            var item = GetByKey(entity.Id);
            items.Remove(item);

            Save();
        }

        public bool Contains(TKey key)
        {
            return items.Any(x => x.Id.Equals(key));
        }

        public void Save()
        {
            fileSystem.WriteToFile(filename, items);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> selector)
        {
            return items.Where(selector.Compile());
        }

        public void Delete(Expression<Func<T, bool>> selector)
        {
            var items = this.items.Where(selector.Compile()).ToList();
            items.ForEach(Delete);
        }

        public TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public string ConvertIdToString(TKey id)
        {
            if (id.Equals(default(TKey)))
            {
                return null;
            }
            return id.ToString();
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }
    }
}