namespace Store.Domain.Products
{
  public class ProductFactory
  {
    public Product Create(ProductId id, Name name)
    {
      return new Product(id, name);
    }

    internal ProductDao ExtractDao(Product product)
    {
      return new ProductDao { ProductId = product.ProductId.Value, Name = product.Name.Value};
    }

    internal Product Load(ProductDao productDao)
    {
      return new Product(new ProductId(productDao.ProductId), new Name(productDao.Name));
    }
  }
}