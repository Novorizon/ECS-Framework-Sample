using Cinemachine;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public struct LoadSceneData
    {
        public string name;
        public LoadSceneMode mode;
        public LoadSceneData(string name, LoadSceneMode mode = LoadSceneMode.Single)
        {
            this.name = name;
            this.mode = mode;
        }
    }
    public class LoadSceneCommand : SimpleCommand
    {
        public const string NAME = "LoadSceneCommand";
        public override void Execute(INotification notification)
        {
            if (notification.Body != null)
            {
                LoadSceneData data = (LoadSceneData)notification.Body;
                if (data.name != null)
                {
                    ResourceManager.Instance.LoadSceneAsync(data.name, OnSceneLoaded, data.mode, true, data);
                }
            }
        }

        public void OnSceneLoaded(Scene scene, object userdata)
        {
            if (userdata is LoadSceneData data)
            {

                //var VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
                //if (VirtualCamera != null)
                //{
                //    var vcam = VirtualCamera.GetComponent<CinemachineVirtualCamera>();
                //    if (vcam != null)
                //    {
                //        //var entity = EntityManager.Create(VirtualCamera.gameObject);
                //        //EntityManager.Instance.AddComponentData<>(entity);
                //    }
                //}
            }

            SendNotification(GameConsts.CMD_GAME_START);
        }
    }
}
