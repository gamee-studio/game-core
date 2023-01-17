using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;
using UnityEngine;

namespace Gamee.Hiuk.Button
{
    public class AudioButton : Singleton<AudioButton>
    {
        [SerializeField] AudioComponent audioButton;
        [SerializeField] Sound soundButton;
        public void PlaySound(Sound sound)
        {
            audioButton.PlaySound(sound);
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
