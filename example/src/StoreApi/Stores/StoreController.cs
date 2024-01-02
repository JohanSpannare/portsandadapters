using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Domain.Stores;
using Store.Stores;

namespace StoreApi.Stores
{
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IStoreService _storeService;

        public StoreController(ILogger<StoreController> logger, IStoreService storeService)
        {
            _logger = logger;
            _storeService = storeService;
        }

        [HttpPut("stores/{storeId}/product/{productId}")]
        public IActionResult AddProductToStore(string storeId, string productId)
        {
            var addProduct = new AddProduct { StoreId = storeId, ProductId = productId };
            var result = _storeService.AddProduct(addProduct);

            

            if (!result) return BadRequest();
            return Ok();
        }
    }
}
