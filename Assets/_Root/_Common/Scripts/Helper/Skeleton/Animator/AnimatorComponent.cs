using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AnimatorComponent : MonoBehaviour
{
    protected Spine.Skeleton skeleton;
    protected Spine.AnimationState animationState;
    public Spine.Skeleton Skeleton => skeleton;
    public Spine.AnimationState AnimationState => animationState;

    protected string animationName;
    public string AnimationName => animationName;

    protected Dictionary<string, Action> cacheEvent = new Dictionary<string, Action>();

    public virtual void Init() 
    {
        animationState.Event += HandleAnimationStateEvent;
    }

    public abstract void Initialize(bool reload = false);
    public abstract void ChangeAnimationName(string animationName);
    public abstract void FlipX(bool isFlipX = false);
    public abstract void FlipY(bool isFlipY = false);
    public void AddAnimation(int trackIndex, string animationName, bool loop, float timeDelay = 0)
    {
        animationState.AddAnimation(trackIndex, animationName, loop, timeDelay);
    }
    public async UniTask<Spine.TrackEntry> PlayAnimation(int trackIndex, string animationName, bool loop = false, float speed = 1)
    {
        this.animationName = animationName;
        await UniTask.WaitUntil(() => animationState != null);
        animationState.TimeScale = speed;
        var trackEntry = animationState.SetAnimation(trackIndex, animationName, loop);
        animationState.Apply(skeleton);
        return trackEntry;
    }

    public void RegisterEvent(string eventName, Action actionEvent = null)
    {
        if (cacheEvent.ContainsKey(eventName))
        {
            cacheEvent[eventName] = actionEvent;
        }
        else
        {
            cacheEvent.Add(eventName, actionEvent);
        }
    }

    protected void HandleAnimationStateEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        Action action = null;
        if(cacheEvent.TryGetValue(e.Data.Name, out action)) 
        {
            action?.Invoke();
        }
    }

    public void StopAnimation()
    {
        animationState.TimeScale = 0;
    }
}