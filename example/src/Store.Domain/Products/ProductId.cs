namespace Store.Domain.Products
{
  public class ProductId
  {
    public string Value { get; }

    public ProductId(string value)
    {
      Value = value;
    }
  }
}