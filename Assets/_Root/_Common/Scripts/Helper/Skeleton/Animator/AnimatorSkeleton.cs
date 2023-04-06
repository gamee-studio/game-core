using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkeletonAnimation))]
public class AnimatorSkeleton : AnimatorComponent
{
    [SerializeField] SkeletonAnimation skeletonAnimation;

    public override void Init()
    {
        skeleton = skeletonAnimation.Skeleton;
        animationState = skeletonAnimation.AnimationState;
        base.Init();
    }

    public override void Initialize(bool reload = false)
    {
        skeletonAnimation.Initialize(reload);
    }
    public override void ChangeAnimationName(string animationName)
    {
        skeletonAnimation.AnimationName = animationName;
    }

    public override void FlipX(bool isFlipX = false)
    {
        skeletonAnimation.initialFlipX = isFlipX;
    }

    public override void FlipY(bool isFlipY = false)
    {
        skeletonAnimation.initialFlipY = isFlipY;
    }
}
