using System.Collections.Generic;
using System.Linq;
using Store.Domain.Products;

namespace StoreApi.Infrastructure.Storage
{
  public class InMemoryProductStorage : IProductStorage
  {
    private readonly List<ProductDao> _productDaos;

    public InMemoryProductStorage()
    {
      _productDaos = new List<ProductDao>();
    }

    public ProductDao Get(string productId)
    {
      return _productDaos.FirstOrDefault(x => x.ProductId == productId);
    }

    public void Store(ProductDao productDao)
    {
      var dao = _productDaos.FirstOrDefault(x => x.ProductId == productDao.ProductId);
      if (dao != null)
      {
        _productDaos.Remove(dao);
        return;
      }

      _productDaos.Add(productDao);
    }
  }
}