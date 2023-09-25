using System.Collections.Generic;
using System.Linq;
using Store.Domain.Stores;

namespace StoreApi.Infrastructure.Storage
{
  public class FakeInMemoryStoreStorage : IStoreStorage
  {
    private readonly List<StoreDao> _storeDaos;

    public FakeInMemoryStoreStorage()
    {
      _storeDaos = new List<StoreDao>();
    }

    public StoreDao Get(string storeId)
    {
      var dao = _storeDaos.SingleOrDefault(x => x.StoreId == storeId);
      
      //FAKE
      if (dao == null)
      {
        Store(new StoreDao { StoreId = storeId, Name = $"{storeId}Name", ProductIds = new string[0]});
      }
      
      return _storeDaos.Single(x => x.StoreId == storeId);
    }

    public void Store(StoreDao storeDao)
    {
      var dao = _storeDaos.SingleOrDefault(x => x.StoreId == storeDao.StoreId);
      if (dao != null)
      {
        _storeDaos.Remove(dao);
      }

      _storeDaos.Add(storeDao);
    }
  }
}