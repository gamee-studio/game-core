using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip audio;
    [SerializeField, Range(0, 1)] float value = 1;

    public AudioClip Audio => audio;
    public float Value => value;
}

