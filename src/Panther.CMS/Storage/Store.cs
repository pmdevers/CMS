using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage
{
    public class Store<T, TKey>
        : IStore<T, TKey> where T : IEntity<TKey>
    {
        private bool _disposed;
        private string _filename;
        private string _content;

        private static object objLock = new object();

        protected IPantherFileSystem FileSystem;
        protected List<T> Items;

        public Store(IPantherFileSystem fileSystem)
        {
            lock (objLock)
            {
                FileSystem = fileSystem;
                _filename = FileSystem.CreateFilename(typeof(T));
                _content = string.Empty;

                //if (!FileSystem.FileExists(_filename))
                //{
                //    FileSystem.CreateFile(_filename);
                //}

                try
                {
                    Items = FileSystem.ReadFile<List<T>>(_filename);
                }
                catch {
                    Items = new List<T>();
                    Save();
                }
            }
        }

        public T GetByKey(TKey key)
        {
            return Items.FirstOrDefault(x => x.Id.Equals(key));
        }

        public void Add(T entity)
        {
            if (Contains(entity.Id))
                return;

            entity.Id = GenerateKey();

            Items.Add(entity);
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
            var index = Items.IndexOf(item);
            Items[index] = entity;

            Save();
        }

        public void Delete(T entity)
        {
            if (!Contains(entity.Id))
                return;

            var item = GetByKey(entity.Id);
            Items.Remove(item);

            Save();
        }

        public bool Contains(TKey key)
        {
            return Items.Any(x => x.Id.Equals(key));
        }

        public void Save()
        {
            FileSystem.WriteToFile(_filename, Items);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> selector)
        {
            return Items.Where(selector.Compile());
        }

        public void Delete(Expression<Func<T, bool>> selector)
        {
            var items = Items.Where(selector.Compile()).ToList();
            items.ForEach(Delete);
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public virtual string ConvertIdToString(TKey id)
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