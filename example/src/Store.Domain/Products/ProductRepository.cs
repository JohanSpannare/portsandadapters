namespace Store.Domain.Products
{
  public class ProductRepository
  {
    private readonly IProductBaseGateway _productBaseGateway;
    private readonly IProductStorage _productStorage;
    private readonly ProductFactory _productFactory;

    public ProductRepository(IProductBaseGateway productBaseGateway, IProductStorage productStorage)
    {
      _productBaseGateway = productBaseGateway;
      _productStorage = productStorage;
      _productFactory = new ProductFactory();
    }

    public Product Get(ProductId productId)
    {
      var pId = productId.Value;

      var getProductBaseResult = _productBaseGateway.Get(new GetProductBase { ProductId = pId });
      if (getProductBaseResult == null) return null;

      var productDao = _productStorage.Get(getProductBaseResult.ProductId);
      if (productDao == null)
      {
        var id = getProductBaseResult.ProductId;
        var name = getProductBaseResult.Name;
        
        var product = _productFactory.Create(new ProductId(id), new Name(name));
        productDao = _productFactory.ExtractDao(product);
        _productStorage.Store(productDao);
      }

      return _productFactory.Load(productDao);
    }
  }
}