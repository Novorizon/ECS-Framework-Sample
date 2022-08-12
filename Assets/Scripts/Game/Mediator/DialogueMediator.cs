using ECS;
using Game;
using MVC.UI;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace Game
{
    public class DialogueMediator : Mediator
    {
        new public static string NAME = typeof(DialogueMediator).FullName;

        private DialogueWindow window;

        public DialogueMediator(object viewComponent) : base(NAME, viewComponent) { }

        public override void OnRegister()
        {
            base.OnRegister();
            window = ViewComponent as DialogueWindow;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                GameConsts.QUEST_SELECT,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            //QuestState state =(QuestState) notification.Body;
            switch (notification.Name)
            {
                //case GameConsts.QUEST_SELECT:
                //    break;
            }
        }
    }
}