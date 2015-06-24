using System;

namespace Panther.CMS.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}