using UnityEngine;

namespace Gamee.Hiuk.Game.Input 
{
    public interface ITouch
    {
        void TouchBegan(Vector2 pos);
        void TouchEnded(Vector2 pos);
        void TouchMoved(Vector2 pos);
    }
}

