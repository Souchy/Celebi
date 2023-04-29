using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services.meta;
using Spark;

namespace souchy.celebi.spark.controllers.meta
{
    [ApiController]
    [Route(Routes.Meta + "shop")]
    public class ShopController : ControllerBase
    {

        private readonly ShopProductService productService;
        private readonly ShopCurrencyService currencyService;
        public ShopController(ShopProductService products, ShopCurrencyService currencies)
        {
            productService = products;
            currencyService = currencies;
        }

        [HttpGet("products")]
        public async Task<List<ShopProduct>> GetShopProducts() => await productService.GetAsync();
        [HttpGet("currencies")]
        public async Task<List<ShopCurrency>> GetShopCurrencies() => await currencyService.GetAsync();


    }
}
