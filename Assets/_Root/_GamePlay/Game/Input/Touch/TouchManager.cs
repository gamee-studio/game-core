namespace Gamee.Hiuk.Game.Input
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class TouchManager : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] private GameObject trailPrefab;
        [SerializeField] private LayerMask layerInput;
        [SerializeField] private float distane = 10f;
        bool isTouched = false;
        bool isRun = false;
        ITouch touch = null;
        GameObject trail;
        public Vector2 PosMouseOnScreen => cam == null ? Vector2.zero : cam.ScreenToWorldPoint(Input.mousePosition);

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
        }
        public void Defaut()
        {
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
            }
            if (Input.GetMouseButtonUp(0))
            {
                isTouched = false;
                touch = null;
                trail.gameObject.SetActive(false);
            }

            if (isTouched)
            {
                Vector2 ray = PosMouseOnScreen;
                trail.transform.position = ray;
                trail.gameObject.SetActive(true);

                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, distane, layerInput);
                if (hit.collider != null)
                {
                    touch = hit.collider.gameObject.GetComponentInParent<ITouch>();
                    if (touch == null) return;
                    touch.Touch(PosMouseOnScreen);
                }
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
