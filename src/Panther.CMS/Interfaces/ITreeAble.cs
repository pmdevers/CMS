using System.Collections.Generic;

namespace Panther.CMS.Interfaces
{
    public interface ITreeAble<T, TKey>
        where TKey : struct
        where T : IEntity<TKey>
    {
        IList<T> Children { get; set; }

        TKey Id { get; set; }

        T Parent { get; set; }

        TKey? ParentId { get; set; }
    }
}