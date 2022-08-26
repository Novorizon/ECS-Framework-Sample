using ECS;
using Game.Input;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public enum ControllerType
    {
        None,
        PressStart,
        PressEnd,
        PressCancel,
        Move
    }
    public class ControllerSelectSystem : ComponentSystem<PlayerController>
    {
        ControllerType controller;
        private float inputValue;

        public override void OnInitialized()
        {
            base.OnInitialized();

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
            GameInput.Controller.Default.Press.started -= PressStart;
            GameInput.Controller.Default.Press.canceled += PressCancel;
#elif UNITY_ANDROID || UNITY_IOS
            GameInput.Controller.Default.VirtualPadLeft.performed += PressStart;
            GameInput.Controller.Default.VirtualPadLeft.canceled += PressCancel;
#endif
        }

        private void PressStart(InputAction.CallbackContext context)
        {
            inputValue = context.ReadValue<float>();
            controller = ControllerType.PressStart;
        }

        private void PressCancel(InputAction.CallbackContext context)
        {
        }


        protected override void OnUpdate(int index, Entity entity, PlayerController player)
        {
            if (inputValue == 0)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                EntityManager.Instance.TryGetEntity(hit.collider.gameObject, out Entity target);
                if (target == null)
                    return;

                if (!EntityManager.Instance.HasComponent<EntityLayer>(target))
                    return;


                EntityLayer layer = EntityManager.Instance.GetComponent<EntityLayer>(target);

                if (layer.Has(EntityLayerMask.Player))
                {
                    if (layer.Has(EntityLayerMask.Friend))// 判断距离 范围内： 选中绿色光环 跑上前 菜单（谈话 查看装备）
                    {

                    }
                    else if (layer.Has(EntityLayerMask.Enemy))//选中 设置为攻击目标
                    {

                    }
                }
                else if (layer.Has(EntityLayerMask.Npc))
                {
                    if (layer.Has(EntityLayerMask.Friend))//选中 绿色光环 跑上前 菜单（谈话 查看装备）
                    {
                        Debug.LogError(target.gameObject.name);
                        target.gameObject.transform.rotation = Quaternion.RotateTowards(target.gameObject.transform.rotation, entity.gameObject.transform.rotation, 1);
                        SendNotification(GameConsts.QUEST_SELECT, target);
                    }
                    else if (layer.Has(EntityLayerMask.Enemy))//选中 光环红色 设置为攻击目标
                    {

                    }

                    if (layer.Has(EntityLayerMask.Interactive))
                    {
                        if (layer.Has(EntityLayerMask.Select))
                        {
                            // 增加组件
                        }
                        else if (layer.Has(EntityLayerMask.Attack))
                        {
                            // 增加组件 设置当前目标
                        }
                        else if (layer.Has(EntityLayerMask.Dialogue))
                        {
                            // 增加组件  目标检测是否靠近 弹出对话框
                            SendNotification(GameConsts.QUEST_SELECT, target);

                        }
                        else if (layer.Has(EntityLayerMask.Pickup))
                        {
                            // 增加组件 目前检测是否靠近 捡起
                        }
                        else if (layer.Has(EntityLayerMask.Follow))
                        {
                            // 增加组件 设置跟随目标
                        }

                    }
                }
                else if (layer.Has(EntityLayerMask.Static))
                {
                    if (layer.Has(EntityLayerMask.Interactive))
                    {
                        // 设置 选中光环黄色
                        if (layer.Has(EntityLayerMask.Select))
                        {

                        }
                        else if (layer.Has(EntityLayerMask.Attack))
                        {

                        }
                        else if (layer.Has(EntityLayerMask.Pickup))
                        {

                        }
                    }



                }
            }


            inputValue = 0;
        }
    }
}
