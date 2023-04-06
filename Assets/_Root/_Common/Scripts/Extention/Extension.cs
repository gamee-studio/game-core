using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
namespace Gamee.Hiuk.Common
{
    public static class Extension
    {
        public static void Clear(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
        public static float GetAnimationLenght(this Animator animator, string animationName, float speedAnimation = 1)
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name.Equals(animationName)) { return clip.length / speedAnimation; }
            }
            return 4f;
        }
        public static void OnCompleted(this Spine.TrackEntry entry, Action actionCompleted)
        {
            entry.Complete += _ => { actionCompleted?.Invoke(); };
        }
        public async static void OnCompleted(this UniTask<Spine.TrackEntry> entry, Action actionCompleted)
        {
            var result = await entry;
            void Call(Spine.TrackEntry _)
            {
                result.Complete -= Call;
                actionCompleted?.Invoke();
            }
            result.Complete += Call;
        }
        public static void DoScale(this GameObject go, float scale, float timeScale = 0.25f, float timeBack = 0.15f, Action actionCompleted = null, Ease ease = Ease.InExpo)
        {
            go.transform.DOKill(true);
            Vector3 scaleDeaut = go.transform.localScale;
            go.transform.DOScale(scaleDeaut * scale, timeScale).SetEase(ease).OnComplete(() =>
            {
                go.transform.DOScale(scaleDeaut, timeBack).OnComplete(() => actionCompleted?.Invoke());
            });
        }
        public static void DoDelay(this GameObject go, float time = 1f, bool isScale = false)
        {
            go.transform.DOKill(true);
            go.SetActive(false);
            DOTween.Sequence().SetDelay(time).OnComplete(() =>
            {
                if (isScale) go.DoScale(1.25f);
                go.SetActive(true);
            });
        }
    }
}

