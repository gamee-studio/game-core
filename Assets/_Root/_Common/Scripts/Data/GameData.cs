using Gamee.Hiuk.Adapter;

namespace Gamee.Hiuk.Data
{
    public static class GameData
    {
        private const string key = "com.gamee.gamebase";
        #region game
        public static System.Action<int> ActionCoinValueChange;
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
            ActionCoinValueChange?.Invoke(coin);
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
        public static bool IsNotShowUpdateAgain
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_not_show_update_again", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_not_show_update_again", value);
        }
        public static string VersionShowedUpdate
        {
            get => PlayerPrefsAdapter.GetString(key + "version_showed_update", UnityEngine.Application.version);
            set => PlayerPrefsAdapter.SetString(key + "version_showed_update", value);
        }
        public static int LevelShowedRateCount
        {
            get => PlayerPrefsAdapter.GetInt(key + "level_showed_rate_count", 0);
            set => PlayerPrefsAdapter.SetInt(key + "level_showed_rate_count", value);
        }
        public static bool IsShowedRate
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_showed_rate", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_showed_rate", value);
        }
        public static string IDPlayerSkinCurrent
        {
            get => PlayerPrefsAdapter.GetString(key + "id_player_skin_current", "");
            set => PlayerPrefsAdapter.SetString(key + "id_player_skin_current", value);
        }
        public static string IDPrincessSkinCurrent
        {
            get => PlayerPrefsAdapter.GetString(key + "id_princess_skin_current", "");
            set => PlayerPrefsAdapter.SetString(key + "id_princess_skin_current", value);
        }
        public static string IDPinSkinCurrent
        {
            get => PlayerPrefsAdapter.GetString(key + "id_pin_skin_current", "");
            set => PlayerPrefsAdapter.SetString(key + "id_pin_skin_current", value);
        }
        #endregion
        #region iap
        public static bool IsRemoveAds
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_remove_ads", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_remove_ads", value);
        }
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
        #region rank
        public static string CountryCode
        {
            get => PlayerPrefsAdapter.GetString(key + "country_code");
            set => PlayerPrefsAdapter.SetString(key + "country_code", value);
        }
        public static string UserName
        {
            get => PlayerPrefsAdapter.GetString(key + "user_name");
            set => PlayerPrefsAdapter.SetString(key + "user_name", value);
        }
        public static int IndexCountryCurrent
        {
            get => PlayerPrefsAdapter.GetInt(key + "index_country_current");
            set => PlayerPrefsAdapter.SetInt(key + "index_country_current", value);
        }
        public static string PlayerID
        {
            get => PlayerPrefsAdapter.GetString(key + "player_id");
            set => PlayerPrefsAdapter.SetString(key + "player_id", value);
        }
        public static string customID
        {
            get => PlayerPrefsAdapter.GetString(key + "custom_id");
            set => PlayerPrefsAdapter.SetString(key + "custom_id", value);
        }
        public static string CustomID
        {
            get
            {
                if (string.IsNullOrEmpty(customID))
                {
                    customID = System.Guid.NewGuid().ToString() + " time: " + System.DateTime.Now;
                }
                return customID;
            }
        }
        #endregion
        #region language
        public static string IDLanguageCurrent
        {
            get => PlayerPrefsAdapter.GetString(key + "id_language_current", "");
            set => PlayerPrefsAdapter.SetString(key + "id_language_current", value);
        }
        #endregion
    }
}