using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Level.Player 
{
    public interface IPlayer
    {
        void Init();
        void Idle();
        void Win();
        void Lose();
    }
}

