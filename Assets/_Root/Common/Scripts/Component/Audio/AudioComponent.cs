using Gamee.Hiuk.Data;
using UnityEngine;
namespace Gamee.Hiuk.Component 
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        public void PlaySound(Sound sound)
        {
            if (!GameData.IsOnAudio) return;
            audioSource.PlayOneShot(sound.Audio, sound.Volume);
        }

        public void Pause() { audioSource.Pause(); }
        public void Resume() { audioSource.UnPause(); }
        public void PlaySoundBackGround(Sound sound)
        {
            if (!GameData.IsOnMusic) return;
            audioSource.clip = sound.Audio;
            audioSource.volume = sound.Volume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}

