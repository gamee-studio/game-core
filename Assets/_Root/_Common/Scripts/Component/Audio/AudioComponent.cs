using Gamee.Hiuk.Data;
using System.Collections;
using UnityEngine;
namespace Gamee.Hiuk.Component 
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField, Range(0, 1f)] float timeNextPlayWait = 0.25f;
        bool isLoop = false;
        Sound soundCurrent = null;

        WaitForSeconds waitTime;
        Coroutine coroutine;

        public void Awake()
        {
            waitTime = new WaitForSeconds(timeNextPlayWait);
        }
        public void PlaySound(Sound sound)
        {
            if (!this.gameObject.activeInHierarchy) return;
            if (sound == null) return;
            if (sound.Audio == null) return;

            if (soundCurrent != null && sound.Audio == soundCurrent.Audio)
            {
                isLoop = true;
            }
            else
            {
                isLoop = false;
                soundCurrent = sound;
            }
            coroutine = StartCoroutine(WaitPlaySoundTime());

            if (isLoop) return;
            if (!GameData.IsOnAudio) return;
            audioSource.PlayOneShot(sound.Audio, sound.Volume);
        }
        public void Reset()
        {
            isLoop = false;
            soundCurrent = null;
        }
        public void Pause() { if(audioSource.isPlaying) audioSource.Pause(); }
        public void Resume() { audioSource.UnPause(); }
        public void Stop() { audioSource.Stop(); }
        public void PlaySoundBackGround(Sound sound)
        {
            if (sound == null) return;
            if (sound.Audio == null) return;

            if (!GameData.IsOnMusic) return;
            audioSource.clip = sound.Audio;
            audioSource.volume = sound.Volume;
            audioSource.loop = true;
            audioSource.Play();
        }
        IEnumerator WaitPlaySoundTime() 
        {
            yield return waitTime;
            Reset();
        }
        private void OnDisable()
        {
            Reset();
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }
}

