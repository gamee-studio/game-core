using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Component 
{
    public class Collisider2DComponent : MonoBehaviour
    {
        public Action<Collider2D> ActionTriggerEnter2D;
        public Action<Collider2D> ActionTriggerStaty2D;
        public Action<Collider2D> ActionTriggerExit2D;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ActionTriggerEnter2D?.Invoke(collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            ActionTriggerStaty2D?.Invoke(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            ActionTriggerExit2D?.Invoke(collision);
        }
    }
}

