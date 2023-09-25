namespace Store.Domain.Products
{
  public interface IProductStorage
  {
    ProductDao Get(string productId);
    void Store(ProductDao productDao);
  }
}