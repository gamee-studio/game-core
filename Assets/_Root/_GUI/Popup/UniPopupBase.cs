#pragma warning disable 0649
using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace Gamee.Hiuk.Popup
{
    public class UniPopupBase  : MonoBehaviour, IPopupHandler
    {
        [SerializeField] private Canvas canvas;
        
        #region Implementation of IUniPopupHandler

        public GameObject ThisGameObject => gameObject;
        public Canvas Canvas => canvas;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void UpdateSortingOrder(int sortingOrder)
        {
            canvas.sortingOrder = sortingOrder;
        }
        #endregion
    }
}