using ECS;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    /// <summary>
    /// 角色面向鼠标所指的方向
    /// </summary>
    public class ControllerLookAtSystem : ComponentSystem<Position, MoveDirection, FaceDirection, PlayerController>
    {
        private Vector2 inputValue;

        public override void OnInitialized()
        {
            base.OnInitialized();

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
            GameInput.Controller.Default.MousePosition.performed += OnMouseMove;
#elif UNITY_ANDROID || UNITY_IOS
            InputManager.Instance.Controller.Default.VirtualPadRight.performed += OnGamePadMove;
#endif
        }


#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
        void OnMouseMove(InputAction.CallbackContext ctx)
        {
            inputValue = ctx.ReadValue<Vector2>();
        }
#elif UNITY_ANDROID || UNITY_IOS
        void OnGamePadMove(InputAction.CallbackContext ctx)
        {
            inputValue = ctx.ReadValue<Vector2>();
        }
#endif

        protected override void OnUpdate(int index, Entity entity, Position position, MoveDirection move, FaceDirection face, PlayerController player)
        {
            //face.Value = move.Value;
//#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
//            var ray = Camera.main.ScreenPointToRay(inputValue);
//            if (Physics.Raycast(ray, out var hit, 100))//, 1 << LayerMask.NameToLayer("Ground")))
//            {
//                var faceTo = (hit.point - (Vector3)position.Value);
//                face.Value = Vector3.ProjectOnPlane(faceTo, Vector3.up).normalized;
//                toward.Value = face.Value;
//            }
//            else
//            {

//            }
//#elif UNITY_ANDROID || UNITY_IOS
//                    var toward = Camera.main.transform.rotation * inputValue;
//                    face.Value = Vector3.ProjectOnPlane(toward, Vector3.up).normalized;
//#endif
        }
    }
}
