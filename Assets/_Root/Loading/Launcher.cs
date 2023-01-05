using Cysharp.Threading.Tasks;
using UnityEngine;

public class Launcher : MonoBehaviour
{
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
    }
    public async UniTask LoadFileData()
    {
        await UniTask.RunOnThreadPool(() =>
        {
        });
    }
}
