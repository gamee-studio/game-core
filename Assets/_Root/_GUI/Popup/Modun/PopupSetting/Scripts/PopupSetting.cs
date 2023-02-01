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
        [SerializeField] SettingItem audioItem;
        [SerializeField] SettingItem musicItem;
        [SerializeField] SettingItem vibrateItem;
        private Action actionclose;

        public void Initialize(Action actionClose = null)
        {
            this.actionclose = actionClose;

            audioItem.Sellect(GameData.IsOnAudio);
            musicItem.Sellect(GameData.IsOnMusic);
            vibrateItem.Sellect(GameData.IsOnVibration);

            audioItem.ActionSellectItem = OnSellectAudioItem;
            musicItem.ActionSellectItem = OnSellectMusicItem;
            vibrateItem.ActionSellectItem = OnSellectVibrateItem;
        }

        void OnSellectAudioItem(bool isOn)
        {
            GameData.IsOnAudio = isOn;
        }
        void OnSellectMusicItem(bool isOn)
        {
            GameData.IsOnMusic = isOn;
        }
        void OnSellectVibrateItem(bool isOn)
        {
            GameData.IsOnVibration = isOn;
        }
        public void Back() 
        {
            actionclose?.Invoke();
            Close();
        }
    }
}

