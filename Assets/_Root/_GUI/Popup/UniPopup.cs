#pragma warning disable 0649
using DG.Tweening;
using UnityEngine;
using System.Collections;
using System;

namespace Gamee.Hiuk.Popup
{
    public class UniPopup  : UniPopupBase
    {
        [SerializeField] protected Sound soundOpen;
        public override void Show()
        {
            base.Show();
            AudioPopup.Play(soundOpen);
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