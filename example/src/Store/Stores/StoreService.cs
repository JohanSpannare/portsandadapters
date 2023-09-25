using Store.Domain.Products;
using Store.Domain.Stores;

namespace Store.Stores
{
  public interface IStoreService
  {
    bool AddProduct(AddProduct addProduct);
  }

  public class StoreService : IStoreService
  {
    private readonly StoreRepository _storeRepository;
    private readonly ProductRepository _productRepository;

    public StoreService
      (
        IStoreStorage storeStorage,
        IProductBaseGateway productBaseGateway,
        IProductStorage productStorage
      )
    {
      _storeRepository = new StoreRepository(storeStorage);
      _productRepository = new ProductRepository(productBaseGateway, productStorage);
    }

    public bool AddProduct(AddProduct addProduct)
    {
      var storeId = new StoreId(addProduct.StoreId);
      var store = _storeRepository.Get(storeId);
      if (store == null)
      {
        return false;
      }

      var productId = new ProductId(addProduct.ProductId);
      var product = _productRepository.Get(productId);
      if (product == null)
      {
        return false;
      }

      if (!store.AddProduct(product))
      {
        return false;
      }

      _storeRepository.Save(store);
      return true;
    }
  }
}
