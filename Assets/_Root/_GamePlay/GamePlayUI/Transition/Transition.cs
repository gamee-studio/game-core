using Gamee.Hiuk.Common;
using Spine.Unity;
using System;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] AnimatorComponent transitionAnimator;
    [SerializeField, SpineAnimation] string animDefaut;
    [SerializeField, SpineAnimation] string animOpen;
    [SerializeField, SpineAnimation] string animClose;

    public void Init()
    {
        transitionAnimator.Init();
    }
    public void Defaut() 
    {
        _= transitionAnimator.PlayAnimation(0, animDefaut);
    }
    public void Open(Action actionCompleted = null) 
    {
        this.gameObject.SetActive(true);
        transitionAnimator.PlayAnimation(0, animOpen).OnCompleted(actionCompleted);
    }
    public void Close(Action actionCompleted = null) 
    {
        this.gameObject.SetActive(true);
        transitionAnimator.PlayAnimation(0, animClose).OnCompleted(actionCompleted);
    }
}
