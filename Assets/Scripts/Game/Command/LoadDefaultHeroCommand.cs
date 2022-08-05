using DataBase;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadDefaultHeroCommand : SimpleCommand
    {
        public const string NAME = "LoadDefaultHeroCommand";
        public override void Execute(INotification notification)
        {
            
        }

        public void LoadProfile()
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            if (tableProxy == null)
                return;

            HeroProxy proxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (proxy == null)
                return;


            DefaultData defaultData = tableProxy.GetData<DefaultData>(1001);
            proxy.SetData(defaultData);
            HeroVO data = proxy.GetData();
            ModelData model = tableProxy.GetData<ModelData>(data.modelId);

            ResourceManager.Instance.LoadAssetAsync<GameObject>(model.name, (asset, _) =>
            {
                GameObject go = GameObject.Instantiate(asset);

                go.transform.position = data.position;
                go.transform.forward = data.forward;

                Debug.LogError(asset.name);

                //SendNotification();//×°±¸
            });



        }
    }
}
