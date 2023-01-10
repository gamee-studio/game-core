using Gamee.Hiuk.Game;
using Gamee.Hiuk.GamePlay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.GamePlay 
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] GamePlayUI gamePlayUI;
        [SerializeField] GameManager gamemanager;

        public GameManager GameManager => gamemanager;
        public GamePlayUI GamePlayUI => gamePlayUI;

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            gamemanager.Play();
            gamePlayUI.DefautUI();
        }
        public void Init() 
        {
            gamemanager.Init(this);
            gamePlayUI.Init(this);
        }
    }
}

