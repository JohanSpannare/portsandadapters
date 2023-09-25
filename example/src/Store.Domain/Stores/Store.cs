using System.Collections.Generic;
using System.Linq;
using Store.Domain.Products;

namespace Store.Domain.Stores
{
  public class Store
  {

    private readonly IList<ProductId> _productIds;

    public StoreId StoreId { get; }
    public Name Name { get; }
    public ProductId[] ProductIds => _productIds.ToArray();

    internal Store(StoreId storeId, Name name, IList<ProductId> productIds)
    {
      StoreId = storeId;
      Name = name;
      _productIds = productIds;
    }

    public bool AddProduct(Product product)
    {
      var productId = product.ProductId;
      if (ProductIds.Any(x => x.Value == productId.Value))
      {
        return false;
      }

      _productIds.Add(productId);
      return true;
    }
  }
}