using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Popup.Setting 
{
    public class SettingItem : MonoBehaviour
    {
        [SerializeField] GameObject btnActive;
        [SerializeField] GameObject btnUnactive;
        [SerializeField] GameObject iconActive;
        [SerializeField] GameObject iconUnactive;

        public Action<bool> ActionSellectItem;
        public void Sellect(bool isOn)
        {
            btnActive.gameObject.SetActive(isOn);
            btnUnactive.gameObject.SetActive(!isOn);
            iconActive.gameObject.SetActive(isOn);
            iconUnactive.gameObject.SetActive(!isOn);

            ActionSellectItem?.Invoke(isOn);
        }
    }
}

