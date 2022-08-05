using Cinemachine;
using DataBase;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadHeroCommand : SimpleCommand
    {
        public const string NAME = "LoadHeroCommand";
        public override void Execute(INotification notification)
        {
            LoadProfile();
        }

        public void LoadProfile()
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            if (tableProxy == null)
                return;

            HeroProxy proxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (proxy == null)
                return;

            int isCreated = PlayerPrefs.GetInt("CharacterCreated", 0);
            if (isCreated == 0)
            {
                DefaultData defaultData = tableProxy.GetData<DefaultData>(10001);
                if (defaultData == null)
                    return;
                proxy.SetData(defaultData);
            }

            HeroVO data = proxy.GetData();
            if (data == null)
                return;

            ModelData model = tableProxy.GetData<ModelData>(data.modelId);
            if (model == null)
                return;
            ResourceManager.Instance.LoadAssetAsync<GameObject>(model.name, (asset, _) =>
            {
                GameObject go = GameObject.Instantiate(asset);

                go.transform.position = data.position;
                go.transform.forward = data.forward;

                Entity hero = EntityManager.Create(go);
                EntityManager.Instance.AddComponentData<LocalToWorld>(hero);
                EntityManager.Instance.AddComponentData<Position>(hero).Value = data.position;
                EntityManager.Instance.AddComponentData<Rotation>(hero).Value = Quaternion.identity;
                EntityManager.Instance.AddComponentData<Scale>(hero).Value = Vector3.one;
                EntityManager.Instance.AddComponentData<CopyToTransformComponent>(hero);

                EntityManager.Instance.AddComponentData<Speed>(hero).Value = data.speed;
                EntityManager.Instance.AddComponentData<MoveDirection>(hero).Value =Vector3.zero;
                EntityManager.Instance.AddComponentData<FaceDirection>(hero).Value = Vector3.forward;// data.forward;

                EntityManager.Instance.AddComponentData<PlayerController>(hero);
                //Debug.LogError(asset.name);


                var VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
                if (VirtualCamera != null)
                {
                    VirtualCamera.Follow = go.transform;
                    VirtualCamera.LookAt = go.transform;
                }
                //SendNotification();//×°±¸
            });



        }
    }
}
