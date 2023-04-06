using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Data;
namespace Gamee.Hiuk.IAP 
{
    public static class IAPAdapter
    {
        public static void BuyPack1() 
        {
        }
        public static void BuyPack2() 
        {
        }
        public static void BuyRemoveAds() 
        {
            GameData.IsRemoveAds = true;
            GameData.IsRemoveInterAds = true;
            GameData.IsRemoveBannerAds = true;

            AdsManager.SetRemoveBanner(true);
            AdsManager.SetRemoveInter(true);
        }
        public static void BuyX2Coin() 
        {
            GameData.X2CoinValue = 2;
        }
        public static void BuyUnlockAllSkin()
        {
            
        }
        public static void BuyCombo() 
        {
            GameData.IsRemoveInterAds = true;
            GameData.IsRemoveBannerAds = true;
            GameData.X2CoinValue = 2;
        }
    }
}

