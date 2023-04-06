using Gamee.Hiuk.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Review;
namespace Gamee.Hiuk.AppLink 
{
    public class ApplinkManager : Singleton<ApplinkManager>
    {
        private string appId = "com.gamee.gamebase";
        private string url;

        public void Start()
        {
            appId = Application.identifier;
#if UNITY_EDITOR
            url = "https://play.google.com/store/apps/details?id=" + appId;
#elif UNITY_ANDROID
        try 
        {
            url = "market://details?id=" + appId;
        }
        catch (System.Exception e)
        {
            url = "https://play.google.com/store/apps/details?id=" + appId;
        }
        StartCoroutine(InitReview());
#elif UNITY_IOS
        try 
        {
            url = "market://details?id=" + appId;
        }
        catch (System.Exception e)
        {
            url = "https://apps.apple.com/us/app/id1562329957";
        }
#endif
        }

        public void OpenApp()
        {
            Application.OpenURL(url);
        }

        public void OpenApp(string appId)
        {
#if UNITY_EDITOR
            url = "https://play.google.com/store/apps/details?id=" + appId;
#elif UNITY_ANDROID
        try 
        {
            url = "market://details?id=" + appId;
        }
        catch (System.Exception e)
        {
            url = "https://play.google.com/store/apps/details?id=" + appId;
        }
#elif UNITY_IOS
        try 
        {
            url = "market://details?id=" + appId;
        }
        catch (System.Exception e)
        {
            url = "https://apps.apple.com/us/app/id1562329957";
        }
#endif
            Application.OpenURL(url);
        }

        public void RateApp()
        {
#if UNITY_EDITOR
            OpenApp();
#elif UNITY_ANDROID
            if (IsRequestReviewCompleted) StartCoroutine(Submit());
            else OpenApp();
#elif UNITY_IOS
            OpenApp();
#endif
        }

#if UNITY_ANDROID
        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;
        private bool IsRequestReviewCompleted => _playReviewInfo != null;

        IEnumerator InitReview()
        {
            if (_reviewManager == null) _reviewManager = new ReviewManager();

            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                yield break;
            }
            _playReviewInfo = requestFlowOperation.GetResult();
        }

        public IEnumerator Submit()
        {
            yield return new WaitUntil(() => _playReviewInfo != null);
            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            _playReviewInfo = null;
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                yield break;
            }
        }
#endif

        #region static api
        public static void Open() { Instance.OpenApp(); }
        public static void Open(string appId) { Instance.OpenApp(appId); }
        public static void Rate() { Instance.RateApp(); }
        #endregion
    }
}