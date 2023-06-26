using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
    [SerializeField] bool isAuto = true;
    [SerializeField] float scale = 1.15f;
    [SerializeField] Ease ease = Ease.Linear;

    Vector3 scaleDefaut = Vector3.one;
    public void Awake()
    {
        scaleDefaut = transform.localScale;
        if (isAuto) Play();
    }

    public void Play()
    {
        transform.localScale = scaleDefaut;
        transform.DOScale(scaleDefaut * scale, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }
    public void Default() 
    {
        this.transform.DOKill();
        transform.localScale = scaleDefaut;
    }
}
