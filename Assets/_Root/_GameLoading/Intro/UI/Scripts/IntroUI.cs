using Gamee.Hiuk.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Loading.Intro 
{
    public class IntroUI : MonoBehaviour
    {
        [SerializeField] private Animator action;
        [SerializeField] private string actionName = "Action";
        [SerializeField] private GameObject btnSkipIntro;
        [SerializeField] private float timeShowSkipButton = 1f;

        Action actionDone;
        Coroutine coroutine;
        public void Run(Action actionCompleted = null) 
        {
            this.actionDone = actionCompleted;

            btnSkipIntro.gameObject.SetActive(false);
            StartCoroutine(ShowButtonSkip());
            action.Play(actionName);
            coroutine = StartCoroutine(WaitTime(action.GetAnimationLenght(actionName), () =>
            {
                actionCompleted?.Invoke();
            }));
        }

        public void Skip() 
        {
            this.actionDone?.Invoke();
            StopCoroutine(coroutine);
        }
        IEnumerator ShowButtonSkip()
        {
            yield return new WaitForSeconds(timeShowSkipButton);
            btnSkipIntro.gameObject.SetActive(true);
        }

        IEnumerator WaitTime(float time, Action actionConpleted)
        {
            yield return new WaitForSeconds(time);
            actionConpleted?.Invoke();
        }

        private void OnDisable()
        {
            StopCoroutine(ShowButtonSkip());
        }
    }
}

