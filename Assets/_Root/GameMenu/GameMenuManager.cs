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

        private void Awake()
        {
            Init();
        }

        public void Init() 
        {
            gameMenuUI.ActionStartGame = OnStartGame;
        }

        void OnStartGame() 
        {
            SceneManager.LoadScene(2);
        }
    }
}

