namespace Gamee.Hiuk.Game.Input
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class TouchManager : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] private GameObject trailPrefab;
        [SerializeField] private LayerMask layerInput;
        [SerializeField] private float distane = 10f;
        [SerializeField] private bool isMultiTouch = false;
        bool isTouched = false;
        bool isRun = false;
        ITouch touchCurrent = null;
        GameObject trail;
        public Vector2 PosMouseOnScreen => cam == null ? Vector2.zero : cam.ScreenToWorldPoint(Input.mousePosition);
        public Action<RaycastHit2D, Vector2> ActionTouchStart;
        public Action<RaycastHit2D, Vector2> ActionTouchMove;
        public Action<RaycastHit2D, Vector2> ActionTouchEnd;

        public void Awake()
        {
            if (cam == null) cam = Camera.main;

            if (trail == null) trail = Instantiate(trailPrefab);
            trail.gameObject.SetActive(false);
        }
        public void Start()
        {
            Defaut();
        }
        public void Run(bool isRun)
        {
            this.isRun = isRun;
            isTouched = false;
        }
        public void Defaut()
        {
            Input.multiTouchEnabled = isMultiTouch;
            isRun = true; 
            isTouched = false;
            trail.gameObject.SetActive(false);
        }
        public void Update()
        {
            if (!isRun) return;
            if (cam == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUIObject()) return;
                if (isTouched) return;
                isTouched = true;
                RaycastHit2D hit = Physics2D.Raycast(PosMouseOnScreen, Vector2.zero, distane, layerInput);
                ActionTouchStart?.Invoke(hit, PosMouseOnScreen);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isTouched = false;
                if(touchCurrent != null) 
                {
                    touchCurrent.TouchEnded(PosMouseOnScreen);
                    touchCurrent = null;
                }
                trail.gameObject.SetActive(false);

                RaycastHit2D hit = Physics2D.Raycast(PosMouseOnScreen, Vector2.zero, distane, layerInput);
                ActionTouchEnd?.Invoke(hit, PosMouseOnScreen);
            }

            if (isTouched)
            {
                trail.transform.position = PosMouseOnScreen;
                trail.gameObject.SetActive(true);

                RaycastHit2D hit = Physics2D.Raycast(PosMouseOnScreen, Vector2.zero, distane, layerInput);

                if (touchCurrent != null)
                {
                    touchCurrent.TouchMoved(PosMouseOnScreen);
                }

                if (hit.collider != null)
                {
                    var touch = hit.collider.gameObject.GetComponentInParent<ITouch>();
                    if (touch != null) 
                    {
                        touch.TouchBegan(PosMouseOnScreen);
                        touchCurrent = touch;
                    }
                }
                ActionTouchMove?.Invoke(hit, PosMouseOnScreen);
            }
        }

        private bool IsPointerOverUIObject()
        {
            if (EventSystem.current == null) return false;
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results); return results.Count > 0;
        }
    }
}
