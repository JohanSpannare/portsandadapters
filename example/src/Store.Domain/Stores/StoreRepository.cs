namespace Store.Domain.Stores
{
  public class StoreRepository
  {
    private readonly IStoreStorage _storeStorage;
    private readonly StoreFactory _storeFactory;

    public StoreRepository(IStoreStorage storeStorage)
    {
      _storeStorage = storeStorage;
      _storeFactory = new StoreFactory();
    }

    public Store Get(StoreId storeId)
    {
      var dao = _storeStorage.Get(storeId.Value);
      return dao == null ? 
        null : 
        _storeFactory.Load(dao);
    }

    public void Save(Store store)
    {
      var storeDao = _storeFactory.ExtractDao(store);
      _storeStorage.Store(storeDao);
    }
  }
}