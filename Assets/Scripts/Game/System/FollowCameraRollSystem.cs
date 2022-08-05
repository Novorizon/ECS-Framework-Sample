using Cinemachine;
using ECS;
using Game.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    public class FollowCameraRollSystem : ComponentSystem<VirtualCamera, RollSensitivity>
    {
        private Vector2 rollDelta;
        private bool isPressed;

        private CinemachineBrain brain;

        public override void OnInitialized()
        {
            base.OnInitialized();

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
            GameInput.Controller.Default.Look.performed += OnLook;
            GameInput.Controller.Default.Look.canceled += OnLook;

            GameInput.Controller.Default.Press.started += OnPressed;
            GameInput.Controller.Default.Press.canceled += OnPressed;

#elif UNITY_ANDROID || UNITY_IOS
           GameInput.Controller.Touch.FirstTouch.performed += FirstTouch_performed;
           GameInput.Controller.Touch.SecondTouch.performed += SecondTouch_performed;

#endif
        }


#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
        void OnLook(InputAction.CallbackContext ctx)
        {
            rollDelta = ctx.ReadValue<Vector2>();
        }

        void OnPressed(InputAction.CallbackContext ctx)
        {
            isPressed = !ctx.canceled && !EventSystem.current.IsPointerOverGameObject();
        }

#elif UNITY_ANDROID || UNITY_IOS

        private bool firstTouchDown, secondTouchDown;

        private void SecondTouch_performed(InputAction.CallbackContext ctx)
        {
            var touchState = ctx.ReadValue<TouchState>();

            if (touchState.phase == TouchPhase.Ended)
            {
                secondTouchDown = false;
                rollDelta = Vector2.zero;
            }
            else if (touchState.phase == TouchPhase.Moved)
            {
                secondTouchDown = !EventSystem.current.IsPointerOverGameObject(touchState.touchId);
                isPressed = firstTouchDown || secondTouchDown;
                if (isPressed && !firstTouchDown)
                {
                    rollDelta = touchState.delta;
                }
            }
        }

        private void FirstTouch_performed(InputAction.CallbackContext ctx)
        {
            var touchState = ctx.ReadValue<TouchState>();
            if (touchState.phase == TouchPhase.Ended)
            {
                firstTouchDown = false;
                rollDelta = Vector2.zero;
            }
            else if (touchState.phase == TouchPhase.Moved)
            {
                firstTouchDown = !EventSystem.current.IsPointerOverGameObject(touchState.touchId);
                isPressed = firstTouchDown || secondTouchDown;
                if (isPressed && firstTouchDown)
                {
                    rollDelta = touchState.delta;
                }
            }
        }
#endif

        protected override void OnUpdate(int index, Entity entity, VirtualCamera virtualCamera, RollSensitivity sensitivity)
        {
            if (!isPressed)
                return;

            if (brain == null || !brain.isActiveAndEnabled)
                brain = Object.FindObjectOfType<CinemachineBrain>();

            if (brain == null || !brain.isActiveAndEnabled)
                return;

            if ((Object)brain.ActiveVirtualCamera != virtualCamera.Value)
                return;

            if (virtualCamera.Value.Follow == null)
                return;

            var roll = rollDelta * sensitivity.Value;

            virtualCamera.Value.Follow.RotateAround(virtualCamera.Value.Follow.position, Vector3.up, roll.x);
            var wantedRotation = virtualCamera.Value.Follow.rotation * Quaternion.Euler(new Vector3(-roll.y, 0, 0));
            if (wantedRotation.eulerAngles.x < 75)
                virtualCamera.Value.Follow.rotation = wantedRotation;
        }
    }
}