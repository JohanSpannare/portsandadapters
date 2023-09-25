using Store.Domain.Products;

namespace StoreApi.Infrastructure.Gateways
{
  public class FakeProductBaseGateway : IProductBaseGateway
  {
    public GetProductBaseResult Get(GetProductBase getProductBase)
    {
      var productId = getProductBase.ProductId;
      return new GetProductBaseResult
      {
        ProductId = productId,
        Name = $"{productId}Name"
      };
    }
  }
}