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

        public void Run(Action actionCompleted = null) 
        {
            btnSkipIntro.gameObject.SetActive(false);
            StartCoroutine(ShowButtonSkip());
            action.Play(actionName);

            actionCompleted?.Invoke();
        }

        IEnumerator ShowButtonSkip()
        {
            yield return new WaitForSeconds(timeShowSkipButton);
            btnSkipIntro.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            StopCoroutine(ShowButtonSkip());
        }
    }
}

