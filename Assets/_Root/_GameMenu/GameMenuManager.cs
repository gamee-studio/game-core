using Gamee.Hiuk.Component;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebaseRemoteConfig;
using Gamee.Hiuk.GameMenu.UI;
using Gamee.Hiuk.IAP;
using Gamee.Hiuk.Popup.Update;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamee.Hiuk.GameMenu 
{
    public class GameMenuManager : MonoBehaviour
    {
        [SerializeField] GameMenuUI gameMenuUI;
        [SerializeField, SceneProperty] string sceneGamePlayName;

        [Header("Audio"), SerializeField] AudioComponent audioGameMenu;
        [SerializeField] Sound soundBg;

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            PlaySoundBG();
            CheckUpdate();
        }
        public void Init() 
        {
            gameMenuUI.Init();
            gameMenuUI.ActionStartGame = OnStartGame;
            gameMenuUI.ActionUpdateMusic = OnUpdateMusic;
            gameMenuUI.ActionRemoveAds = OnRemoveAds;

            UpdateButtonRemoveAds();
        }
        void PlaySoundBG()
        {
            audioGameMenu.PlaySoundBackGround(soundBg);
        }
        void CheckUpdate() 
        {
            if (GameData.LevelCurrent >= GameConfig.LevelShowUpdateCount)
            {
                string version = RemoteConfig.VersionApp;
                if (UpdateCheck.ConvertVersionValue(GameData.VersionShowedUpdate) < UpdateCheck.ConvertVersionValue(version)) 
                {
                    GameData.VersionShowedUpdate = version;
                    GameData.IsNotShowUpdateAgain = false;
                }

                if (GameData.IsNotShowUpdateAgain) return;
                if (UpdateCheck.CheckVersion(version))
                {
                    gameMenuUI.ShowPopupNewUpdate((isNotShow)=> { GameData.IsNotShowUpdateAgain = isNotShow; },
                        RemoteConfig.DescritptionApp, version);
                }
            }
        }
        void UpdateButtonRemoveAds() 
        {
            gameMenuUI.UpdateButtonRemoveAds(!GameData.IsRemoveAds);
        }
        void OnStartGame() 
        {
            SceneManager.LoadScene(sceneGamePlayName);
        }
        void OnUpdateMusic(bool isOnMusic) 
        {
            if (!isOnMusic) audioGameMenu.Pause();
            else PlaySoundBG();
        }
        void OnRemoveAds() 
        {
            IAPManager.BuyIAPRemoveAds(() =>
            {
                UpdateButtonRemoveAds();
            });
        }
    }
}

