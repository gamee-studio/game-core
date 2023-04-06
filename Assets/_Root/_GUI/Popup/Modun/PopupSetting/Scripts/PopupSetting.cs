using Gamee.Hiuk.Data;
using Gamee.Hiuk.Popup.Setting;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupSetting : UniPopup
    {
        [SerializeField] TextMeshProUGUI txtVersion;
        [SerializeField] SettingItem audioItem;
        [SerializeField] SettingItem musicItem;
        [SerializeField] SettingItem vibrateItem;
        private Action<bool> actionUpdateMusic;
        private Action actionClose;

        public void Initialize(Action<bool> actionUpdateMusic, Action actionClose)
        {
            this.actionClose = actionClose;
            this.actionUpdateMusic = actionUpdateMusic;

            audioItem.Sellect(GameData.IsOnAudio);
            musicItem.Sellect(GameData.IsOnMusic);
            vibrateItem.Sellect(GameData.IsOnVibration);

            audioItem.ActionSellectItem = OnSellectAudioItem;
            musicItem.ActionSellectItem = OnSellectMusicItem;
            vibrateItem.ActionSellectItem = OnSellectVibrateItem;
            txtVersion.text = "Version " + Application.version;
        }

        void OnSellectAudioItem(bool isOn)
        {
            GameData.IsOnAudio = isOn;
        }
        void OnSellectMusicItem(bool isOn)
        {
            if (GameData.IsOnMusic == isOn) return;
            GameData.IsOnMusic = isOn;
            actionUpdateMusic?.Invoke(isOn);
        }
        void OnSellectVibrateItem(bool isOn)
        {
            GameData.IsOnVibration = isOn;
        }
        public void Back() 
        {
            actionClose?.Invoke();
            Close();
        }
    }
}

