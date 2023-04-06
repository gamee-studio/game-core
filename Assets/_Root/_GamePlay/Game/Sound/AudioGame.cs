using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;
using UnityEngine;

public class AudioGame : Singleton<AudioGame>
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