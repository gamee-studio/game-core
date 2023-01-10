using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Game.Loader 
{
    [CreateAssetMenu(fileName = "GameLoadDataResource", menuName = "Scripttableobject/GameLoadDataResource", order = 1)]
    public class GameLoadDataResource : ScriptableObject
    {
        const string path = "GameLoadDataResource";
        private static GameLoadDataResource instance;
        public static GameLoadDataResource Instance => instance ??= Resources.Load<GameLoadDataResource>(path);

        [SerializeField] List<LevelLoadData> listLevelLoadData;
        public void Init() 
        {
            foreach (var levelLoadData in listLevelLoadData)
            {
                levelLoadData.Init();
            }
        }
        public LevelLoadData GetLevelData(ELevelLoadType levelLoadType)
        {
            foreach(var levelLoadData in listLevelLoadData) 
            {
                if (levelLoadData.Type.Equals(levelLoadType)) return levelLoadData;
            }
            return null;
        }

        #region static api
        public static LevelLoadData GetLevelDataCurrent(ELevelLoadType levelLoadType) 
        {
            return Instance.GetLevelData(levelLoadType);
        }
        public static void InitData() 
        {
            Instance.Init();
        }
        #endregion
    }
}
