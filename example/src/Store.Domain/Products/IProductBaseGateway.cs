namespace Store.Domain.Products
{
  public interface IProductBaseGateway
  {
    GetProductBaseResult Get(GetProductBase getProductBase);
  }
}