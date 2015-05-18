using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Extensions
{
    public static class Tree
    {
        public static IEnumerable<T> MakeTree<T, TKey>(this IEnumerable<ITreeAble<T, TKey>> actualObjects)
                where T : ITreeAble<T, TKey>, IEntity<TKey>
                where TKey : struct
        {
            var lookup = new Dictionary<TKey, ITreeAble<T, TKey>>();
            foreach (var obj in actualObjects.ToList())
            {
                lookup.Add(((IEntity<TKey>)obj).Id, obj);
            }
            foreach (var item in lookup.Values)
            {
                ITreeAble<T, TKey> proposedParent;
                TKey parentId = item.ParentId.HasValue ? item.ParentId.Value : default(TKey);
                if (lookup.TryGetValue(parentId, out proposedParent))
                {
                    item.Parent = (T)proposedParent;
                    proposedParent.Children.Add((T)item);
                }
            }
            return lookup.Values.Where(x => x.ParentId == null).Cast<T>();
        }
    }
}