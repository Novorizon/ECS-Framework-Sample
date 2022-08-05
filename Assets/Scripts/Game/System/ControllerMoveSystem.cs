using ECS;
using Game.Input;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class ControllerMoveSystem : ComponentSystem<MoveDirection, FaceDirection, PlayerController>
    {
        private Vector3 inputValue;

        public override void OnInitialized()
        {
            base.OnInitialized();

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
            GameInput.Controller.Default.Move.performed += OnMove;
            GameInput.Controller.Default.Move.canceled += OnMove;
#elif UNITY_ANDROID || UNITY_IOS
            GameInput.Controller.Default.VirtualPadLeft.performed += OnMove;
            GameInput.Controller.Default.VirtualPadLeft.canceled += OnMove;
#endif
        }


        void OnMove(InputAction.CallbackContext ctx)
        {
            var v2 = ctx.ReadValue<Vector2>();
            inputValue = new Vector3(v2.x, 0, v2.y);
        }

        protected override void OnUpdate(int index, Entity entity, MoveDirection move, FaceDirection face, PlayerController player)
        {
            Vector3 toward = Camera.main.transform.rotation * inputValue;
            move.Value = Vector3.ProjectOnPlane(toward, Vector3.up).normalized;
            if (!move.Value.Equals(float3.zero))
                face.Value = move.Value;

        }
    }
}
