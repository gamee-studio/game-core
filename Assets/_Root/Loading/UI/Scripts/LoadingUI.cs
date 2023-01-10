using Gamee.Hiuk.UI.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Loading.UI 
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] ProcessUI processUI;
        [SerializeField] float timeLoad = 3f;

        public void Run(Action actionCompleted = null) 
        {
            processUI.Run(99, 99, timeLoad);
            processUI.ActionCompleted = actionCompleted;
        }

        public void RunCompleted() 
        {
            processUI.Run(100, 100);
        }
    }
}

