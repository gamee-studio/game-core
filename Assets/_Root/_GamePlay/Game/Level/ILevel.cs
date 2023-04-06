using System;
namespace Gamee.Hiuk.Level 
{
    public interface ILevel
    {
        void Win(Action actionCompleted = null);
        void Lose(Action actionCompleted = null);
    }
}

