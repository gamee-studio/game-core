using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;
using UnityEngine;
namespace Gamee.Hiuk.Popup 
{
    public class AudioPopup : Singleton<AudioPopup>
    {
        [SerializeField] AudioComponent audioPopup;
        public void PlaySound(Sound sound)
        {
            audioPopup.PlaySound(sound);
        }
        public static void Play(Sound sound)
        {
            Instance.PlaySound(sound);
        }
    }
}

