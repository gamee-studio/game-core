using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AddressableAssets;

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

        firebaseApp.Init();
    }
    public async UniTask LoadFileData()
    {
        await UniTask.RunOnThreadPool(() =>
        {
        });
    }
}
