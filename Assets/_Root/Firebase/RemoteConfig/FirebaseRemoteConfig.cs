namespace Gamee.Hiuk.FirebaseRemoteConfig
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class FirebaseRemoteConfig : MonoBehaviour
    {
        [SerializeField] RemoteConfigAdapter remoteConfig;
        public void Init()
        {
            remoteConfig.Defaut();
            FetchDataAsync();
        }

        private Task FetchDataAsync()
        {
            Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWith(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                Debug.Log("Fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                Debug.Log("Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("Fetch completed successfully!");
            }

            var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case Firebase.RemoteConfig.LastFetchStatus.Success:
                    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                    info.FetchTime));
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case Firebase.RemoteConfig.FetchFailureReason.Error:
                            Debug.Log("Fetch failed for unknown reason");
                            break;
                        case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                            Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Pending:
                    Debug.Log("Latest Fetch call still pending.");
                    break;
            }

            remoteConfig.FetchData();
            }
        }
    }
