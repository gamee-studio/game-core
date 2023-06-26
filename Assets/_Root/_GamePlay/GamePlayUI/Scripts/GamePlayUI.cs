using Gamee.Hiuk.Popup;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class GamePlayUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txtLevel;
    [SerializeField] protected List<ObjMoveUI> listObjectMove;
    [SerializeField] protected Transition transition;

    protected PopupManager popupManager;

    public Action ActionBackHome;
    public Action ActionReplay;
    public Action<bool> ActionNextLevel;
    public Action ActionSkipLevel;
    public Action ActionBackLevel;
    public Action<bool> ActionProcessFull;

    public virtual void Init()
    {
        popupManager = PopupManager.Instance;
        transition.Init();
        transition.Open();
    }
    public void HideAllPopup()
    {
        popupManager.HideAll();
    }
    public virtual void DefautUI()
    {
        foreach (var obj in listObjectMove)
        {
            obj.Defaut();
        }
    }
    public void MoveUI()
    {
        foreach (var obj in listObjectMove)
        {
            obj.Move();
        }
    }
    public void UpdateTextLevel(string text)
    {
        txtLevel.text = text;
    }
    public abstract void ShowPopupWin(int coinBonus = 0);
    public abstract void ShowPopupLose(bool isShowSkip = false);

    public void ShowPopupRate(Action actionClose = null)
    {
        popupManager.ShowPopupRate(actionClose);
    }
    protected void NextLevelOnWin()
    {
        ActionNextLevel?.Invoke(false);
    }
    protected void NextLevelOnLose()
    {
        ActionNextLevel?.Invoke(true);
    }
    public void ReplayLevel()
    {
        ActionReplay?.Invoke();
    }
    public void SkipLevel()
    {
        ActionSkipLevel?.Invoke();
    }
    public void BackLevel()
    {
        ActionBackLevel?.Invoke();
    }
    public void OnFullProcess(bool isFull)
    {
        ActionProcessFull?.Invoke(isFull);
    }
    public void BackHome()
    {
        ActionBackHome?.Invoke();

    }
    public void TransitionClose(Action actionCompleted = null)
    {
        transition.Close(actionCompleted);
    }
    public void RePlay()
    {
        ActionReplay?.Invoke();
    }
}
