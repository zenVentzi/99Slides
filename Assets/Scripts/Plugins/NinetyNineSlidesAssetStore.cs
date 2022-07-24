using System.Collections.Generic;
using Soomla.Store;

namespace Assets.Scripts.Plugins
{
    class NinetyNineSlidesAssetStore : IStoreAssets
    {
        public const string FullGameId = "full_version";//full_version android.test.purchased
        private const string Description = "By getting this product you agree on having unlimited games to play.";

        private static readonly VirtualGood FullGameVg = new SingleUseVG(
            "Full game",
            Description,
            FullGameId,
            new PurchaseWithMarket(new MarketItem(FullGameId, MarketItem.Consumable.NONCONSUMABLE, 1.5f)));

        #region methods

        public int GetVersion()
        {
            return 1;
        }

        public VirtualCurrency[] GetCurrencies()
        {
            return new VirtualCurrency[] { };
        }

        public VirtualGood[] GetGoods()
        {
            return new[] { FullGameVg };
        }

        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return new VirtualCurrencyPack[] { };
        }

        public VirtualCategory[] GetCategories()
        {
            return new[]
            {
                new VirtualCategory("General", new List<string> {FullGameId})
            };
        }
        #endregion
    }
}
