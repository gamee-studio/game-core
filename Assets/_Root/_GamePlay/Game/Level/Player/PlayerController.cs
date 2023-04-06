using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Level.Player 
{
    public abstract class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] protected EPlayerState state = EPlayerState.PLAYER_NONE;
        public EPlayerState State => state;
        public abstract void Init();
        public abstract void Idle();
        public abstract void Lose();
        public abstract void Win();
        public abstract void Defaut();
        public abstract void UpdateSkin();
    }
}
