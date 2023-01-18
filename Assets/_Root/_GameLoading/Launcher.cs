using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using Gamee.Hiuk.Game.Loader;
using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Data;

public class Launcher : MonoBehaviour
{
    [SerializeField] FirebaseApp firebaseApp;
    private bool isRunCompleted = false;
    public bool IsRunCompleted => isRunCompleted;

    public async UniTask Run()
    {
        isRunCompleted = false;
        LoadData();
        await LoadFileData();
        isRunCompleted = true;
    }

    public void LoadData() 
    {
        Vibration.Init();
        DOTween.Init();
        Addressables.InitializeAsync();

        GameLoader.Init();
        firebaseApp.Init();

        AdsManager.SetRemoveReward(GameData.IsRemoveRewardAds);
        AdsManager.SetRemoveBanner(GameData.IsRemoveBannerAds);
        AdsManager.SetRemoveInter(GameData.IsRemoveInterAds);
    }
    public async UniTask LoadFileData()
    {
        await UniTask.RunOnThreadPool(() =>
        {
        });
    }
}
