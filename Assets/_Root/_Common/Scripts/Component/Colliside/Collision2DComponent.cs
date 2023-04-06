using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Component 
{
    [RequireComponent(typeof(Collision2D))]
    public class Collision2DComponent : MonoBehaviour
    {
        public Action<Collision2D> ActionCollisionEnter2D;
        public Action<Collision2D> ActionCollisionStaty2D;
        public Action<Collision2D> ActionCollisionExit2D;
        Collision2D col2D;
        public Collision2D Col2D => col2D ??= this.GetComponent<Collision2D>();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ActionCollisionEnter2D?.Invoke(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            ActionCollisionStaty2D?.Invoke(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            ActionCollisionExit2D?.Invoke(collision);
        }
        public void Disable()
        {
            if (Col2D == null) return;
            Col2D.collider.enabled = false;
        }
        public void Active() 
        {
            if (Col2D == null) return;
            Col2D.collider.enabled = true;
        }
    }
}

