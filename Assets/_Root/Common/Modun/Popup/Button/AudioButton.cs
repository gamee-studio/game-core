using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;
using UnityEngine;

namespace Gamee.Hiuk.Popup 
{
    public class AudioButton : Singleton<AudioButton>
    {
        [SerializeField] AudioComponent audioComponent;
        [SerializeField] Sound soundButton;
        public void PlaySound(Sound sound)
        {
            audioComponent.PlaySound(sound);
        }
        public static void Play() 
        {
            Instance.PlaySound(Instance.soundButton);
        }
        public static void Play(Sound sound) 
        {
            Instance.PlaySound(sound);
        }
    }
}
