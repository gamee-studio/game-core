using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.UI.Helper
{
    public class ProcessUI : MonoBehaviour
    {
        [SerializeField] EProcessType type;
        [SerializeField] Image process;
        [SerializeField] Ease ease = Ease.Linear;

        [SerializeField] TextMeshProUGUI text;
        [SerializeField] string strFormat = "{0}";

        float valueCurrent = 0;
        float valueCache = 0;
        public Action ActionCompleted;
        public Action ActionUpdate;
        public Action<bool> ActionFull;
        public float ValueCurrent => valueCurrent;
        public void Run(float valueUpdate, float valueMax, float time = 0f)
        {
            var t = DOTween.To(x => valueCache = x, valueCurrent, valueUpdate, time).SetEase(ease);
            valueCurrent = valueUpdate;

            t.OnComplete(() =>
            {
                if (valueCurrent >= valueMax)
                {
                    valueCurrent = valueMax;
                    ActionFull?.Invoke(true);
                }
                else ActionFull?.Invoke(false);
                ActionCompleted?.Invoke();
            });

            t.OnUpdate(() =>
            {
                process.fillAmount = valueCache / valueMax;
                ActionUpdate?.Invoke();

                if (text == null) return;
                switch (type)
                {
                    case EProcessType.PROCESS_PERCENT:
                        text.text = string.Format(strFormat, (int)(valueCache / valueMax * 100));
                        break;
                    case EProcessType.PROCESS_COUNT:
                        text.text = string.Format(strFormat, valueCache + "/" + valueMax);
                        break;
                }
            });
        }
        public void UpdateUI(float valueUpdate, float valueMax) 
        {
            valueCurrent = valueUpdate;
            process.fillAmount = valueUpdate / valueMax;
        }
    }
    public enum EProcessType 
    {
        PROCESS_PERCENT,
        PROCESS_COUNT
    }
}

