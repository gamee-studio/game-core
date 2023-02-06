using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Level.Player 
{
    public abstract class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] EPlayerState state = EPlayerState.PLAYER_NONE;

        public abstract void Init();
        public virtual void Idle() 
        {
            state = EPlayerState.PLAYER_IDLE;
        }
        public virtual void Lose() 
        {
            state = EPlayerState.PLAYER_LOSE;
        }
        public virtual void Win() 
        {
            state = EPlayerState.PLAYER_WIN;
        }
    }
}
