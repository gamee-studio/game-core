using Gamee.Hiuk.Adapter;

namespace Gamee.Hiuk.Data
{
    public static class GameData
    {
        private const string key = "com.gamee.gamebase";

        public static int LevelCurrent
        {
            get => PlayerPrefsAdapter.GetInt(key + "level_current", 1);
            set => PlayerPrefsAdapter.SetInt(key + "level_current", value);
        }
        public static int CoinCurrent
        {
            get => PlayerPrefsAdapter.GetInt(key + "coin_current", 0);
            set => PlayerPrefsAdapter.SetInt(key + "coin_current", value);
        }
        public static void AddCoin(int coin) 
        {
            CoinCurrent += coin * X2CoinValue;
        }
        public static string LevelNameCurrent => "level_" + LevelCurrent;

        public static bool IsNewGame
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_new_game", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_new_game", value);
        }
        public static bool IsShowedIntro
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_showed_intro", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_showed_intro", value);
        }
        #region iap
        public static bool IsRemoveInterAds
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_remove_inter_ads", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_remove_inter_ads", value);
        }
        public static bool IsRemoveRewardAds
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_remove_reward_ads", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_remove_reward_ads", value);
        }
        public static bool IsRemoveBannerAds
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_remove_banner_ads", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_remove_banner_ads", value);
        }
        public static int X2CoinValue 
        {
            get => PlayerPrefsAdapter.GetInt(key + "x2_coin_value", 1);
            set => PlayerPrefsAdapter.SetInt(key + "x2_coin_value", value);
        }
        #endregion
        #region setting
        public static bool IsOnAudio 
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_on_audio", true);
            set => PlayerPrefsAdapter.SetBool(key + "is_on_audio", value);
        }
        public static bool IsOnMusic
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_on_music", true);
            set => PlayerPrefsAdapter.SetBool(key + "is_on_music", value);
        }
        public static bool IsOnVibration
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_on_vibration", true);
            set => PlayerPrefsAdapter.SetBool(key + "is_on_vibration", value);
        }
        #endregion
    }
}