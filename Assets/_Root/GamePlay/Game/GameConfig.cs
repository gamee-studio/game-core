using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Game 
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Scripttableobject/GameConfig", order = 1 )]
    public class GameConfig : ScriptableObject
    {
        const string path = "GameConfig";
        private static GameConfig instance;
        public static GameConfig Instance => instance ??= Resources.Load<GameConfig>(path);

        #region static api
        #endregion
    }
}

