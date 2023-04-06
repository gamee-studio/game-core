using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Spine.Unity;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnimationComponent : MonoBehaviour
{
    [SerializeField] internal List<AnimationItem> listAnimation;

    public AnimationItem GetAnimation(string animName) 
    {
        return listAnimation.FirstOrDefault(_=> _.Anim.Equals(animName));
    }

#if UNITY_EDITOR
    public SkeletonDataAsset GetDataAsset() 
    {
        SkeletonAnimation skeletonAnimation = this.GetComponentInChildren<SkeletonAnimation>();
        if (skeletonAnimation != null) return skeletonAnimation.SkeletonDataAsset;
        SkeletonGraphic skeletonGraphic = this.GetComponentInChildren<SkeletonGraphic>();
        if (skeletonGraphic != null) return skeletonGraphic.SkeletonDataAsset;
        return null;
    }
    public GameObject Obj => this.gameObject;
#endif
}

[System.Serializable]
public class AnimationItem 
{
    [SerializeField, ReadOnly] string anim;
    [SerializeField] bool isLoop;
    [SerializeField] Sound sound;

    public Sound Sound => sound;
    public string Anim => anim;
    public bool IsLoop => isLoop;

    public AnimationItem(string anim) 
    {
        this.anim = anim;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AnimationComponent), true)]
[CanEditMultipleObjects]
public class AnimationComponentEditor : Editor
{
    private AnimationComponent animationComponent;


    protected void OnEnable()
    {
        animationComponent = (AnimationComponent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update", GUILayout.MinHeight(40), GUILayout.MinWidth(50)))
        {
            SkeletonDataAsset skeletonDataAsset = animationComponent.GetDataAsset();
            if (skeletonDataAsset == null) return;
            foreach (var anim in skeletonDataAsset.GetSkeletonData(false).Animations)
            {
                if(!animationComponent.listAnimation.Any(_ => _.Anim.Equals(anim.Name)))
                {
                    AnimationItem animationItem = new AnimationItem(anim.Name);
                    animationComponent.listAnimation.Add(animationItem);
                }
            }
            EditorUtility.SetDirty(animationComponent.Obj);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
