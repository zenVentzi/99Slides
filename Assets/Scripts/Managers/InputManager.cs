using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public delegate void OnPlayerSlideEventHandler(Directions direction);
    public class InputManager : MyMono
    {
        public static InputManager Instance;

        private Vector2 endTouchPos;
        private Vector2 firstTouchPos;
        private const int MinSlidedDistance = 30;
        private GameObject touchedButton;

        public bool HasSlided { get; private set; }

        public OnPlayerSlideEventHandler OnPlayerSlideEvent;

        [UsedImplicitly]
        private void Awake()
        {
            touchedButton = default(GameObject);
            Instance = this;
        }

        [UsedImplicitly]
        private void Update()
        {
            UpdateSlideState();
            UpdateTouchEvents();
        }

        private void UpdateTouchEvents()
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
               if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
                {
                    touchedButton = SortingLayerManager.GetTopmostBelowMouse();

                    if (touchedButton != null)
                        touchedButton.SendMessage("OnnMouseDown");
                }
                else if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended) && touchedButton != null)
                {
                    touchedButton.SendMessage("OnnMouseUp");
                    touchedButton = null;
                }
            }
#elif UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                touchedButton = SortingLayerManager.GetTopmostBelowMouse();

                if (touchedButton != null)
                    touchedButton.SendMessage("OnnMouseDown");
            }
            else if (Input.GetMouseButtonUp(0) && touchedButton != null)
            {
                touchedButton.SendMessage("OnnMouseUp");
                touchedButton = null;
            }
#endif
        }

        private void UpdateSlideState()
        {
#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0))
            {
                firstTouchPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchPos = Input.mousePosition;
                OnTouchEnd();
            }
            else if (!Input.GetMouseButton(0))
            {
                firstTouchPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }
#elif UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        break;
                    case TouchPhase.Ended:
                        endTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        OnTouchEnd();
                        break;
                }
            }
            else if (firstTouchPos != Vector2.zero)
            {
                firstTouchPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }
#endif
        }

        private void OnTouchEnd()
        {
            var slidedDistance = Vector2.Distance(firstTouchPos, endTouchPos);

            if (slidedDistance > MinSlidedDistance && OnPlayerSlideEvent != null)
            {
                OnPlayerSlideEvent(GetSlidedDirection());
                HasSlided = true;
            }
            else
            {
                HasSlided = false;
            }
        }

        private Directions GetSlidedDirection()
        {
            if (!(Vector2.Distance(firstTouchPos, endTouchPos) > MinSlidedDistance) || endTouchPos == Vector2.zero)
                return Directions.None;

            var difference = endTouchPos - firstTouchPos;

            if (Mathf.Abs(difference.x) >= Mathf.Abs(difference.y))
            {
                return difference.x > 0 ? Directions.Right : Directions.Left;
            }

            return difference.y > 0 ? Directions.Up : Directions.Down;
        }
    }
}
