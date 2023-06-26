using Gamee.Hiuk.Game;
using Gamee.Hiuk.GamePlay.UI;
using UnityEngine;

namespace Gamee.Hiuk.GamePlay 
{
    public class 
        GamePlayManagerNormal : GamePlayManager
    {
        [SerializeField] CameraMain cameraMain;
        GamePlayUINormal gamePlayUINormal;
        protected override void Init()
        {
            cameraMain.Init();
            gamePlayUINormal = (GamePlayUINormal)gamePlayUI;
            base.Init();
        }
        #region game
        #endregion

        #region ui
        protected override void OnBackHome() 
        {
            if (!IsPlaying) return;
            gamePlayUI.TransitionClose(() =>
            {
                base.OnBackHome();
            });
        }
        #endregion
    }
}

