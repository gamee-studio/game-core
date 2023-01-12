using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Component 
{
    public class Collision2DComponent : MonoBehaviour
    {
        public Action<Collision2D> ActionCollisionEnter2D;
        public Action<Collision2D> ActionCollisionStaty2D;
        public Action<Collision2D> ActionCollisionExit2D;
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
    }
}

