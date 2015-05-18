namespace Panther.CMS.Interfaces
{
    public interface IPantherStoreFactory
    {
        IStore<T, TKey> GetStorage<T, TKey>() where T : IEntity<TKey>;

        T GetStorage<T>() where T : IStore;
    }
}