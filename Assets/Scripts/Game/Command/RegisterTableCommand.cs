using DataBase;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class RegisterTableCommand : SimpleCommand
    {
        public const string NAME = "RegisterTableCommand";
        public override void Execute(INotification notification)
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            tableProxy.RegisterTable<HeroTableAccess>();
            tableProxy.RegisterTable<ModelTableAccess>();
            tableProxy.RegisterTable<DefaultTableAccess>();
            SendNotification(GameConsts.LOAD_DB);
        }
    }
}
