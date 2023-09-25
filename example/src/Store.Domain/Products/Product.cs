namespace Store.Domain.Products
{
  public class Product
  {
    public ProductId ProductId { get; }
    public Name Name { get; }

    internal Product(ProductId productId, Name name)
    {
      ProductId = productId;
      Name = name;
    }
  }
}