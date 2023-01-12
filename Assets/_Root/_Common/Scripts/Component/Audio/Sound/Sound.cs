using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] AudioClip audio;
    [SerializeField, Range(0, 1)] float volume = 1;

    public AudioClip Audio => audio;
    public float Volume => volume;
}

