using System.Linq;
using Store.Domain.Products;

namespace Store.Domain.Stores
{
  public class StoreFactory
  {
    internal Store Load(StoreDao storeDao)
    {
      var productIds = storeDao.ProductIds.Select(x => new ProductId(x)).ToList();
      return new Store(new StoreId(storeDao.StoreId), new Name(storeDao.Name), productIds);
    }

    internal StoreDao ExtractDao(Store store)
    {
      var productIds = store.ProductIds.Select(x => x.Value).ToArray();
      return new StoreDao {StoreId = store.StoreId.Value, Name = store.Name.Value, ProductIds = productIds};
    }
  }
}