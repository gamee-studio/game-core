using Gamee.Hiuk.Adapter;

namespace Gamee.Hiuk.Data
{
    public static class GameData
    {
        private const string key = "com.gamee.gamebase";

        public static int LevelCurrent
        {
            get => PlayerPrefsAdapter.GetInt(key + "level_current");
            set => PlayerPrefsAdapter.SetInt(key + "level_current", value);
        }

        public static bool IsShowedIntro
        {
            get => PlayerPrefsAdapter.GetBool(key + "is_showed_intro", false);
            set => PlayerPrefsAdapter.SetBool(key + "is_showed_intro", value);
        }

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