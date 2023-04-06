using Gamee.Hiuk.Adapter;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Gamee.Hiuk.Game.Loader
{
    [System.Serializable]
    public class LevelLoadData
    {
        [SerializeField] ELevelLoadType type;
        [SerializeField] string pathLevel;
        [SerializeField] string levelName;
        [SerializeField] int levelMax = 100;
        [SerializeField] int levelLoopStart = 1;
        [SerializeField, Range(0, 5)] float timeDelayWin = 2;
        [SerializeField, Range(0, 5)] float timeDelayLose = 1;
        [SerializeField, Range(1, 10)] float cameraRoomSize = 5f;
        [SerializeField, GUID] string id;

        public ELevelLoadType Type => type;
        public string PathLevel => pathLevel;
        public string LevelNameCurrent => levelName + "_" + LevelIndex;
        public int LevelIndex => listLevel[Index];
        public float TimeDelayWin => timeDelayWin;
        public float TimeDelayLose => timeDelayLose;
        public int LevelMax => levelMax - LevelStartLoop + 1;
        int LevelStartLoop => IsFirstLooped ? (levelLoopStart >= 1 ? levelLoopStart : 1) : 1;
        public float CameraRoomSize => cameraRoomSize;

        public int Index 
        {
            set => PlayerPrefsAdapter.SetInt(id + "index", value);
            get => PlayerPrefsAdapter.GetInt(id + "index", 0);
        }
        int LevelMaxOld
        {
            set => PlayerPrefsAdapter.SetInt(id + "level_max_old", value);
            get => PlayerPrefsAdapter.GetInt(id + "level_max_old", 0);
        }
        public bool IsFirstLooped 
        {
            set => PlayerPrefsAdapter.SetBool(id + "is_first_loop", value);
            get => PlayerPrefsAdapter.GetBool(id + "is_first_loop", false);
        }
        List<int> listLevel = new List<int>();

        public void Init() 
        {
            LoadLevelList();
        }
        public void SetIndex(int index) { Index = index; }
        void LoadLevelList() 
        {
            string levelListData = LoadLevelListData();
            if (levelListData == "")
            {
                LoadLevelListDefaut();
            }
            else
            {
                listLevel = JsonAdapter.FromJson<int>(levelListData).ToList();
            }
        }
        void LoadLevelListDefaut()
        {
            listLevel = new List<int>();
            LevelMaxOld = levelMax;
            for (int i = LevelStartLoop; i <= levelMax; i++)
            {
                listLevel.Add(i);
            }
            SaveLevelList();
        }
        string LoadLevelListData() 
        {
            return PlayerPrefsAdapter.GetString(id + "level_list");
        }
        void SaveLevelList() 
        {
            string levelListData = JsonAdapter.ToJson(listLevel.ToArray());
            PlayerPrefsAdapter.SetString(id + "level_list", levelListData);
        }
        public void Uplevel()
        {
            Index++;
            var levelIndex = LevelMaxOld - LevelStartLoop + 1;
            if(Index >= levelIndex) 
            {
                if(!IsFirstLooped) IsFirstLooped = true;

                LoadLevelListDefaut();
                Index = 0;
                ShuffleLevel();
            }
        }
        public void DownLevel() 
        {
            Index--;
        }
        public void ShuffleLevel() 
        {
            Util.Shuffle(listLevel);
            SaveLevelList();
        }
    }
}

