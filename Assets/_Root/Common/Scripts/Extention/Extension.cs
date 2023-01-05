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
    }
}

