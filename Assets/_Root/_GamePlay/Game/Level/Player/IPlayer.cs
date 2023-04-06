using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Level.Player 
{
    public interface IPlayer
    {
        public void Init();
        public void Idle();
        public void Lose();
        public void Win();
        public void Defaut();
        public void UpdateSkin();
    }
}

