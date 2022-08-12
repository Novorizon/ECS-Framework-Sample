using Cinemachine;
using DataBase;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadNPCCommand : SimpleCommand
    {
        public const string NAME = "LoadNPCCommand";
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

            ResourceManager.Instance.LoadAssetAsync<GameObject>("FemaleCharacter", (asset, _) =>
            {
                GameObject go = GameObject.Instantiate(asset);

                go.transform.position = new Vector3(300,0,436);
                go.transform.forward = Vector3.forward;

                Entity hero = EntityManager.Create(go);
                EntityManager.Instance.AddComponentData<LocalToWorld>(hero);
                EntityManager.Instance.AddComponentData<Position>(hero).Value = go.transform.position;
                EntityManager.Instance.AddComponentData<Rotation>(hero).Value = Quaternion.identity;
                EntityManager.Instance.AddComponentData<Scale>(hero).Value = Vector3.one;
                EntityManager.Instance.AddComponentData<CopyToTransformComponent>(hero);

                EntityManager.Instance.AddComponentData<Speed>(hero).Value = 0;
                EntityManager.Instance.AddComponentData<MoveDirection>(hero).Value =Vector3.forward;
                EntityManager.Instance.AddComponentData<FaceDirection>(hero).Value = Vector3.forward;

                EntityManager.Instance.AddComponentData<EntityLayer>(hero).Value= EntityLayerMask.Npc| EntityLayerMask.Friend| EntityLayerMask.Interactive| EntityLayerMask.Select;
                EntityManager.Instance.AddComponentData<Selected>(hero);
                //Debug.LogError(asset.name);

            });



        }
    }
}
