using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Panther.CMS.Interfaces
{
    public interface IStore
    {
        void Save();
    }

    public interface IStore<T, in TKey> : IStore where T : IEntity<TKey>
    {
        T GetByKey(TKey key);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        bool Contains(TKey key);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> selector);
    }
}