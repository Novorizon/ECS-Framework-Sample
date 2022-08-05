using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadDatabaseCommand : SimpleCommand
    {
        public const string NAME = "LoadDatabaseCommand";
        public override void Execute(INotification notification)
        {
            if (notification.Name!=null)
            {
                ResourceManager.Instance.LoadAssetAsync<Object>(notification.Name, (asset, _) =>
                {
                    TextAsset text= asset as TextAsset;
                    Debug.LogError(asset.name);
                    SendNotification(GameConsts.LOAD_DB_FINISH);
                });
            }
        }
    }
}
