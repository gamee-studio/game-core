using Gamee.Hiuk.FirebaseCloundMessage;
using Gamee.Hiuk.FirebaseRemoteConfig;
using Gamee.Hiuk.Pattern;
using UnityEngine;

public class FirebaseApp : Singleton<FirebaseApp>
{
    public FirebaseRemoteConfig remoteConfig;
    public FirebaseCloundMessage firebaseCloundMessage;
    [SerializeField] bool isHaveCloundMessage;

    public void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("[Firebase] init completed!");

                remoteConfig.Init();
                if(isHaveCloundMessage) firebaseCloundMessage.Init();
            }
            else
            {
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
}

