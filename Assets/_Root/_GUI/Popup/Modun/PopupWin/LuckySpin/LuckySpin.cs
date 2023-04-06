using Gamee.Hiuk.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class LuckySpin : MonoBehaviour
{
    [SerializeField] List<SpinItem> listSpinItem;
    [SerializeField] RectTransform anchor;
    [SerializeField] float speed;
    [SerializeField] float delayTimeRotation = 0.2f;
    [SerializeField] float minAngleRotate = 0;
    [SerializeField] float maxAngleRotate = 180;
    [SerializeField] RectTransform font;
    [SerializeField] float speedDeltaMax = 1.5f;
    [SerializeField] float offset = 20f;

    bool isRun = false;
    bool isLeft = false;
    public Action<int> ActionSelectItem;
    float size = 100;
    SpinItem spinItemCurrent;
    bool isInitialized = false;
    float speedDelta => (float)((size / 2 - offset) - Mathf.Abs(anchor.localPosition.x)) > 0 ? (float)((size / 2 - offset) - Mathf.Abs(anchor.localPosition.x)) / (size / 2 - offset) * speedDeltaMax : 1;

    public void Initialize()
    {
        if (!isInitialized) 
        {
            listSpinItem = new List<SpinItem>();
            size = font.sizeDelta.x * font.localScale.x;
            listSpinItem = this.GetComponentsInChildren<SpinItem>().ToList();
            foreach (var spinItem in listSpinItem)
            {
                spinItem.ActionCollisider += OnTriggerItemEvent;
            }
            isInitialized = true;
        }
        Reset();
    }

    private void Update()
    {
        if (!isRun) return;
        //DoRotation();
        DoMove();
    }

    public void Pause(Action<SpinItem> actionRotateCompleted = null)
    {
        if (!isRun) return;

        StartCoroutine(DelayPauseRotation(delayTimeRotation, () =>
        {
            isRun = false;
            actionRotateCompleted?.Invoke(spinItemCurrent);
        }));
    }

    public void Reset()
    {
        isRun = true;
    }
    IEnumerator DelayPauseRotation(float time, Action actionCompleted = null)
    {
        yield return new WaitForSeconds(time);
        actionCompleted?.Invoke();
    }
    void DoRotation()
    {
        anchor.localRotation = Quaternion.Euler(anchor.localRotation.eulerAngles + Vector3.forward * speed * (isLeft ? -1 : 1) * Time.deltaTime);
        if (anchor.localEulerAngles.z >= maxAngleRotate) isLeft = true;
        if (anchor.localEulerAngles.z <= minAngleRotate) isLeft = false;
    }
    void DoMove()
    {
        anchor.localPosition += Vector3.right * (speed * (isLeft ? -1 : 1) * (Time.deltaTime + speedDelta));
        if (anchor.localPosition.x >= (size / 2 - offset)) isLeft = true;
        if (anchor.localPosition.x <= -(size / 2 - offset)) isLeft = false;
    }

    void OnTriggerItemEvent(SpinItem item)
    {
        if(spinItemCurrent != null) spinItemCurrent.UnSellect();
        item.Sellect();
        spinItemCurrent = item;
        ActionSelectItem?.Invoke(item.Value);
    }
}

