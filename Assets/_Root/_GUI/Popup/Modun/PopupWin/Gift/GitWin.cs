using Spine.Unity;
using UnityEngine;

public class GitWin : MonoBehaviour
{
    [SerializeField] AnimatorComponent gitAniamtor;
    [SerializeField, SpineAnimation] string animIdle;
    [SerializeField, SpineAnimation] string animOpen;

    public void Init() { gitAniamtor.Init(); }
    public void Idle() { _= gitAniamtor.PlayAnimation(0, animIdle, true); }

    public void Open() { _ = gitAniamtor.PlayAnimation(0, animOpen); }
}
