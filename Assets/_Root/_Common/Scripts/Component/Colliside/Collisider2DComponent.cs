using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Component 
{
    [RequireComponent(typeof(Collider2D))] 
    public class Collisider2DComponent : MonoBehaviour
    {
        public Action<Collider2D> ActionTriggerEnter2D;
        public Action<Collider2D> ActionTriggerStaty2D;
        public Action<Collider2D> ActionTriggerExit2D;

        Collider2D col2D;
        public Collider2D Col2D => col2D ??= this.GetComponent<Collider2D>();

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

        public void Disable()
        {
            if (Col2D == null) return;
            col2D.enabled = false;
        }
        public void Active() 
        {
            if (Col2D == null) return;
            Col2D.enabled = true;
        }
    }
}

