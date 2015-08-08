using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panther.CMS.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddWhereNotIn<TKey, TValue>(this Dictionary<TKey, TValue> source,
            Dictionary<TKey, TValue> source2)
        {
            foreach (var item in source2.Where(item => !source.ContainsKey(item.Key)))
            {
                source.Add(item.Key, item.Value);
            }
        }
    }
}
