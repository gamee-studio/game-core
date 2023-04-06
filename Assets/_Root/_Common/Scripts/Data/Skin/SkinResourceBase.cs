using Gamee.Hiuk.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Data.Skin 
{
    public abstract class SkinResourceBase<T> : ResourceObject<T> where T : SkinResourceBase<T>
    {
        [SerializeField] protected SkinData skinDefaut;
        [SerializeField] protected List<SkinData> listSkinData;

        protected SkinData GetSkinDefaut() { return skinDefaut; }
        protected virtual List<SkinData> GetAllSkin() 
        {
            return listSkinData;
        }
        public abstract SkinData GetSkinCurrent();
        public abstract void UpdateSkinCurrent(SkinData skinData);
        protected List<SkinData> GetAllSkinNotHas() 
        {
            List<SkinData> listSkinNotHas = new List<SkinData>();
            foreach (var skinData in listSkinData)
            {
                if (!skinData.IsHas) listSkinNotHas.Add(skinData);
            }
            return listSkinNotHas;
        }
        protected SkinData GetSkinByID(string id) 
        {
            foreach(var skinData in listSkinData) 
            {
                if (skinData.ID.Equals(id)) return skinData;
            }
            return skinDefaut;
        }
        protected SkinData GetSkinByName(string skinName)
        {
            foreach (var skinData in listSkinData)
            {
                if (skinData.SkinName.Equals(skinName)) return skinData;
            }
            return skinDefaut;
        }
        protected List<SkinData> GetAllSkinCoin()
        {
            List<SkinData> listSkinCoin = new List<SkinData>();
            foreach (var skinData in listSkinData)
            {
                if (skinData.IsBuyCoin) listSkinCoin.Add(skinData);
            }
            return listSkinCoin;
        }
        protected List<SkinData> GetAllSkinGitBox()
        {
            List<SkinData> listSkinGixBox = new List<SkinData>();
            foreach (var skinData in listSkinData)
            {
                if (skinData.IsGiftBox) listSkinGixBox.Add(skinData);
            }
            return listSkinGixBox;
        }
        protected List<SkinData> GetAllSkinGitBoxNotHas()
        {
            List<SkinData> listSkinGixBoxNotHas = new List<SkinData>();
            foreach (var skinData in listSkinData)
            {
                if (skinData.IsGiftBox && !skinData.IsHas) listSkinGixBoxNotHas.Add(skinData);
            }
            return listSkinGixBoxNotHas;
        }
        #region static api
        public static SkinData GetSkinDefautData() { return Instance.GetSkinDefaut(); }
        public static List<SkinData> GetAllSkinData() { return Instance.GetAllSkin(); }
        public static SkinData GetSkinCurrentData() { return Instance.GetSkinCurrent(); }
        public static List<SkinData> GetAllSkinDataNotHas() { return Instance.GetAllSkinNotHas(); }
        public static SkinData GetSkinDataByID(string id) { return Instance.GetSkinByID(id); }
        public static SkinData GetSkinDataByName(string id) { return Instance.GetSkinByName(id); }
        public static List<SkinData> GetAllSkinDataCoin() { return Instance.GetAllSkinCoin(); }
        public static List<SkinData> GetAllSkinDataGitBox() { return Instance.GetAllSkinGitBox(); }
        public static List<SkinData> GetAllSkinDataGitBoxNotHas() { return Instance.GetAllSkinGitBoxNotHas(); }
        public static void UpdateSkinCurrentData(SkinData skinData) { Instance.UpdateSkinCurrent(skinData); }
        #endregion
    }
}
