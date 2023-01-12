using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.GameMenu.UI 
{
    public class GameMenuUI : MonoBehaviour
    {
        public Action ActionStartGame;

        public void StartGame() 
        {
            ActionStartGame?.Invoke();
        }
        public void DefautUI() { }
        public void MoveUI() { }
    }
}

