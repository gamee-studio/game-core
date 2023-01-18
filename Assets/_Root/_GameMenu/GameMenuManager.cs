using Gamee.Hiuk.Component;
using Gamee.Hiuk.GameMenu.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamee.Hiuk.GameMenu 
{
    public class GameMenuManager : MonoBehaviour
    {
        [SerializeField] GameMenuUI gameMenuUI;

        [Header("Audio"), SerializeField] AudioComponent audioGameMenu;
        [SerializeField] Sound soundBg;

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            PlaySoundBG();
        }
        public void Init() 
        {
            gameMenuUI.Init();
            gameMenuUI.ActionStartGame = OnStartGame;
        }
        void PlaySoundBG()
        {
            audioGameMenu.PlaySoundBackGround(soundBg);
        }

        void OnStartGame() 
        {
            SceneManager.LoadScene(2);
        }
    }
}

