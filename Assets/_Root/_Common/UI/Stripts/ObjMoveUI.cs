using System;
using DG.Tweening;
using UnityEngine;
public class ObjMoveUI : MonoBehaviour
{
    [SerializeField] private EMoveType moveType;
    [SerializeField] private float distane = 500;
    [SerializeField] private float time = .5f;
    [SerializeField] private Ease ease = Ease.InBack;

    private Vector3 positionDefaut = Vector3.zero;
    private RectTransform thisRectTransform;
    private void Awake()
    {
        thisRectTransform = this.GetComponent<RectTransform>();
        positionDefaut = thisRectTransform.anchoredPosition;
    }

    public void Move(Action actionCompleted = null)
    {
        thisRectTransform.DOKill();
        switch (moveType)
        {
            case EMoveType.MOVE_UP:
                thisRectTransform.DOAnchorPosY(positionDefaut.y + distane, time).SetEase(ease).OnComplete(() =>
                {
                    actionCompleted?.Invoke();
                });
                break;
            case EMoveType.MOVE_DOWN:
                thisRectTransform.DOAnchorPosY(positionDefaut.y - distane, time).SetEase(ease).OnComplete(() =>
                {
                    actionCompleted?.Invoke();
                });
                break;
            case EMoveType.MOVE_RIGHT:
                thisRectTransform.DOAnchorPosX(positionDefaut.x + distane, time).SetEase(ease).OnComplete(() =>
                {
                    actionCompleted?.Invoke();
                });
                break;
            case EMoveType.MOVE_LEFT:
                thisRectTransform.DOAnchorPosX(positionDefaut.x - distane, time).SetEase(ease).OnComplete(() =>
                {
                    actionCompleted?.Invoke();
                });
                break;
        }
    }

    public void Defaut()
    {
        if (thisRectTransform == null) return;
        thisRectTransform.anchoredPosition = positionDefaut;
    }
    public void MoveBack()
    {
        thisRectTransform.DOKill();
        thisRectTransform.DOAnchorPos(positionDefaut, time / 2).SetEase(ease);
    }
}

