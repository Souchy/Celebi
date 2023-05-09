using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services.meta;

namespace souchy.celebi.spark.controllers.meta
{
    [ApiController]
    [Produces("application/json")]
    [Route(Routes.Meta + "shop")]
    public class ShopController : ControllerBase
    {

        private readonly ModelProductService models;
        private readonly CurrencyProductService currencies;
        private readonly ConsumableProductService consumables;
        private readonly AccountService accounts;
        private readonly UserManager<Account> accountManager;

        public ShopController(ModelProductService products, CurrencyProductService currencies, ConsumableProductService consumables,
            AccountService accounts, UserManager<Account> accountManager)
        {
            this.models = products;
            this.currencies = currencies;
            this.consumables = consumables;
            this.accounts = accounts;
            this.accountManager = accountManager;
        }

        [HttpGet("modelProducts")]
        public async Task<List<ModelProduct>> GetShopProducts() => await models.GetAsync();
        [HttpGet("consumableProducts")]
        public async Task<List<ConsumableProduct>> GetConsumableProducts() => await consumables.GetAsync();
        [HttpGet("currencyProducts")]
        public async Task<List<CurrencyProduct>> GetShopCurrencies() => await currencies.GetAsync();

        [Authorize]
        [HttpPost("buyModel")]
        public async Task<ActionResult<ModelProduct>> buyModel(ObjectId id)
        {
            // Checks
            var account = await accountManager.GetUserAsync(this.User);
            if (account == null) 
                return BadRequest();
            var product = await models.GetAsync(id);
            if(product == null || account.Info.Currency < product.Currency)
                return Forbid();

            // Transaction
            var trans = new ShopTransaction()
            {
                ProductId = product._id,
                Type = TransactionType.Purchase,
                Price = product.Currency,
                Account = account.Id
            };
            account.Info.Currency -= product.Currency;

            // Apply
            var result = await accounts.UpdateAsync(account.Id, account);
            if (result.ModifiedCount <= 0) 
                return Problem("Unable to update account");
            return Ok(product);
        }

        [Authorize]
        [HttpPost("buyConsumable")]
        public async Task<ActionResult<ModelProduct>> buyConsumable(ObjectId id)
        {
            // Checks
            var account = await accountManager.GetUserAsync(this.User);
            if (account == null)
                return BadRequest();
            var product = await consumables.GetAsync(id);
            if (product == null || account.Info.Currency < product.Currency) 
                return Forbid();

            // Transaction
            var trans = new ShopTransaction() { 
                ProductId = product._id, 
                Type = TransactionType.Purchase,
                Price = product.Currency,
                Account = account.Id
            };
            account.Info.Currency -= product.Currency;

            // Apply
            var result = await accounts.UpdateAsync(account.Id, account);
            if (result.ModifiedCount <= 0)
                return Problem("Unable to update account");
            return Ok(product);
        }

    }
}
