namespace Store.Domain.Stores
{
  public interface IStoreStorage
  {
    StoreDao Get(string storeId);
    void Store(StoreDao storeDao);
  }
}