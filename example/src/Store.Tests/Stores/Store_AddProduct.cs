using NSubstitute;
using Store.Domain.Products;
using Store.Domain.Stores;
using Store.Stores;
using Xunit;

namespace Store.Tests.Stores
{
  public class Store_AddProduct
  {
    private readonly IStoreStorage _storeStorage;
    private readonly IProductBaseGateway _productBaseGateway;
    private readonly IProductStorage _productStorage;
    private readonly StoreService _storeService;

    public Store_AddProduct()
    {
      _storeStorage = Substitute.For<IStoreStorage>();
      _productBaseGateway = Substitute.For<IProductBaseGateway>();
      _productStorage = Substitute.For<IProductStorage>();
      _storeService = new StoreService(_storeStorage, _productBaseGateway, _productStorage);
    }


    [Fact]
    public void AddProduct_StoreAndProductFound_ProductAddedToStore()
    {
      StoreDao storedStoreDao = null;

      _storeStorage.Get("store_id").Returns(new StoreDao { ProductIds = new string[0] });
      _storeStorage.When(x => x.Store(Arg.Any<StoreDao>()))
        .Do(info => { storedStoreDao = (StoreDao)info[0];});

      _productBaseGateway
        .Get(Arg.Is<GetProductBase>(x => x.ProductId == "product_id"))
        .Returns(new GetProductBaseResult
        {
          ProductId = "product_id",
          Name = "product_name"
        });

      _productStorage.Get("product_id").Returns(new ProductDao {ProductId = "product_id" });

      var addProduct = new AddProduct
      {
        StoreId = "store_id",
        ProductId = "product_id"
      };

      _storeService.AddProduct(addProduct);

      Assert.Collection(storedStoreDao.ProductIds, productId =>
      {
        Assert.Equal("product_id", productId);
      });
    }

    [Fact]
    public void AddProduct_ExistingProductCanNotBeFoundInStorage_ProductStoredInStorage()
    {
      ProductDao storedProductDao = null;

      _storeStorage.Get("store_id").Returns(new StoreDao {ProductIds = new string[0]});

      _productBaseGateway
        .Get(Arg.Is<GetProductBase>(x => x.ProductId == "product_id"))
        .Returns(new GetProductBaseResult
        {
          ProductId = "product_id",
          Name = "product_name"
        });

      _productStorage.Get("product_id").Returns((ProductDao) null);
      
      _productStorage
        .When(x => x.Store(Arg.Any<ProductDao>()))
        .Do(info => { storedProductDao = (ProductDao)info[0]; });

      var addProduct = new AddProduct
      {
        StoreId = "store_id",
        ProductId = "product_id"
      };

      _storeService.AddProduct(addProduct);

      Assert.Equal("product_id", storedProductDao.ProductId);
      Assert.Equal("product_name", storedProductDao.Name);
    }

    [Fact]
    public void AddProduct_ExistingProductCanNotBeFoundInStorage_ProductAddedToStore()
    {
      StoreDao storedStoreDao = null;

      _storeStorage.Get("store_id").Returns(new StoreDao { ProductIds = new string[0] });
      _storeStorage.When(x => x.Store(Arg.Any<StoreDao>()))
        .Do(info => { storedStoreDao = (StoreDao)info[0]; });

      _productBaseGateway
        .Get(Arg.Is<GetProductBase>(x => x.ProductId == "product_id"))
        .Returns(new GetProductBaseResult
        {
          ProductId = "product_id",
          Name = "product_name"
        });

      _productStorage.Get("product_id").Returns((ProductDao)null);

      var addProduct = new AddProduct
      {
        StoreId = "store_id",
        ProductId = "product_id"
      };

      _storeService.AddProduct(addProduct);

      Assert.Collection(storedStoreDao.ProductIds, productId =>
      {
        Assert.Equal("product_id", productId);
      });
    }

    [Fact]
    public void AddProduct_ProductFoundFromAlternativelyId_ProductNotUpdatedInStorage()
    {
      ProductDao storedProductDao = null;

      _storeStorage.Get("store_id").Returns(new StoreDao { ProductIds = new string[0] });

      _productBaseGateway
        .Get(Arg.Is<GetProductBase>(x => x.ProductId == "product_alternatively_id"))
        .Returns(new GetProductBaseResult
        {
          ProductId = "product_id",
          Name = "product_name"
        });

      _productStorage.Get("product_id").Returns(new ProductDao {ProductId = "product_id" });

      _productStorage
        .When(x => x.Store(Arg.Any<ProductDao>()))
        .Do(info => { storedProductDao = (ProductDao)info[0]; });

      var addProduct = new AddProduct
      {
        StoreId = "store_id",
        ProductId = "product_alternatively_id"
      };

      _storeService.AddProduct(addProduct);

      Assert.Null(storedProductDao);
    }
  }
}