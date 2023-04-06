using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SkeletonGraphic))]
public class AnimatorSkeletonUI : AnimatorComponent
{
    [SerializeField] SkeletonGraphic skeletonGraphic;

    public override void Init()
    {
        skeleton = skeletonGraphic.Skeleton;
        animationState = skeletonGraphic.AnimationState;
        base.Init();
    }

    public override void Initialize(bool reload = false)
    {
        skeletonGraphic.Initialize(reload);
    }
    public override void ChangeAnimationName(string animationName)
    {
        skeletonGraphic.startingAnimation = animationName;
    }
    public override void FlipX(bool isFlipX = false)
    {
        skeletonGraphic.initialFlipX = isFlipX;
    }

    public override void FlipY(bool isFlipY = false)
    {
        skeletonGraphic.initialFlipY = isFlipY;
    }
}
