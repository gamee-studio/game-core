using Gamee.Hiuk.Component;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebaseRemoteConfig;
using Gamee.Hiuk.Loading.Intro;
using Gamee.Hiuk.Loading.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamee.Hiuk.Loading 
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] LoadingUI loadingUI;
        [SerializeField] Launcher launcher;
        [SerializeField] float timeDelayLoadScene = 0.5f;
        [Header("Audio"), SerializeField] AudioComponent audioLoading;
        [SerializeField] Sound soundBg;

        [Header("Intro")] [SerializeField] bool isShowIntro;
        [SerializeField] IntroUI introUI;

        bool isLoadingUIRunCompleted;
        AsyncOperation _loadScene;

        private void Start()
        {
            PlaySoundBG();

            Application.targetFrameRate = 60;
            LoadNextScene();
            Run();
        }
        void PlaySoundBG()
        {
            audioLoading.PlaySoundBackGround(soundBg);
        }
        private async void Run() 
        {
            isLoadingUIRunCompleted = false;
            loadingUI.Run(() =>
            {
                isLoadingUIRunCompleted = true;
            });

            await launcher.Run();
            StartCoroutine(WaitForLoadMenuScene());
        }

        private IEnumerator WaitForLoadMenuScene()
        {
            yield return new WaitUntil(() => isLoadingUIRunCompleted == true);
            loadingUI.RunCompleted();
            yield return new WaitForSeconds(timeDelayLoadScene);
            if (isShowIntro && !GameData.IsShowedIntro)
            {
                ShowIntro();
            }
            else ShowNextScene();
        }

        private void ShowIntro() 
        {
            loadingUI.gameObject.SetActive(false);
            introUI.gameObject.SetActive(true);

            introUI.Run(() =>
            {
                IntroRunCompleted();
            });
        }
        private void IntroRunCompleted() 
        {
            GameData.IsShowedIntro = true;
            ShowNextScene();
        }
        private void LoadNextScene()
        {
            if (RemoteConfig.IsAutoStartGame) _loadScene = SceneManager.LoadSceneAsync(2);
            else _loadScene = SceneManager.LoadSceneAsync(1);
            _loadScene.allowSceneActivation = false;
        }

        private void ShowNextScene()
        {
#if (!UNITY_EDITOR)
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
#endif
            _loadScene.allowSceneActivation = true;
        }
    }
}

