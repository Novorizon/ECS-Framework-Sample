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
                    if (layer.Has(EntityLayerMask.Friend))// �жϾ��� ��Χ�ڣ� ѡ����ɫ�⻷ ����ǰ �˵���̸�� �鿴װ����
                    {

                    }
                    else if (layer.Has(EntityLayerMask.Enemy))//ѡ�� ����Ϊ����Ŀ��
                    {

                    }
                }
                else if (layer.Has(EntityLayerMask.Npc))
                {
                    if (layer.Has(EntityLayerMask.Friend))//ѡ�� ��ɫ�⻷ ����ǰ �˵���̸�� �鿴װ����
                    {
                        Debug.LogError(target.gameObject.name);
                        target.gameObject.transform.rotation = Quaternion.RotateTowards(target.gameObject.transform.rotation, entity.gameObject.transform.rotation, 1);
                        SendNotification(GameConsts.QUEST_SELECT, target);
                    }
                    else if (layer.Has(EntityLayerMask.Enemy))//ѡ�� �⻷��ɫ ����Ϊ����Ŀ��
                    {

                    }

                    if (layer.Has(EntityLayerMask.Interactive))
                    {
                        if (layer.Has(EntityLayerMask.Select))
                        {
                            // �������
                        }
                        else if (layer.Has(EntityLayerMask.Attack))
                        {
                            // ������� ���õ�ǰĿ��
                        }
                        else if (layer.Has(EntityLayerMask.Dialogue))
                        {
                            // �������  Ŀ�����Ƿ񿿽� �����Ի���
                            SendNotification(GameConsts.QUEST_SELECT, target);

                        }
                        else if (layer.Has(EntityLayerMask.Pickup))
                        {
                            // ������� Ŀǰ����Ƿ񿿽� ����
                        }
                        else if (layer.Has(EntityLayerMask.Follow))
                        {
                            // ������� ���ø���Ŀ��
                        }

                    }
                }
                else if (layer.Has(EntityLayerMask.Static))
                {
                    if (layer.Has(EntityLayerMask.Interactive))
                    {
                        // ���� ѡ�й⻷��ɫ
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
