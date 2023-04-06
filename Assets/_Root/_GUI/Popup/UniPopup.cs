#pragma warning disable 0649
using DG.Tweening;
using UnityEngine;

namespace Gamee.Hiuk.Popup
{
    public class UniPopup  : UniPopupBase
    {
        [SerializeField] RectTransform broad;
        [SerializeField] protected Sound soundOpen;

        Vector3 scaleDefaut = Vector3.one;
        public void Awake()
        {
            if (broad == null) return;
            scaleDefaut = broad.localScale;
        }
        public override void Show()
        {
            base.Show();
            AudioPopup.Play(soundOpen);

            if (broad == null) return;
            broad.localScale = scaleDefaut * 0.95f;
            broad.DOScale(scaleDefaut * 1.05f, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
            {
                broad.DOScale(scaleDefaut, 0.1f);
            });
        }

        public override void Close()
        {
            base.Close();
        }

        public override void UpdateSortingOrder(int sortingOrder)
        {
            Canvas.sortingOrder = sortingOrder;
        }
    }
}