using Gamee.Hiuk.Data;
using UnityEngine;
namespace Gamee.Hiuk.Component 
{
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        public void PlaySound(Sound sound)
        {
            if (!GameData.IsOnAudio) return;
            audioSource.PlayOneShot(sound.Audio, sound.Value);
        }

        public void Pause() { audioSource.Pause(); }
        public void Resume() { audioSource.UnPause(); }
    }
}

