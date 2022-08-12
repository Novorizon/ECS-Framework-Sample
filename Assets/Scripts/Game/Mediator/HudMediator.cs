using Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace Game
{
    public class HudMediator : Mediator
    {
        new public static string NAME = typeof(HudMediator).FullName;

        private HudWindow hudWindow;

        public HudMediator(object viewComponent) : base(NAME, viewComponent) { }

        public override void OnRegister()
        {
            base.OnRegister();
            hudWindow = ViewComponent as HudWindow;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                GameConsts.HUD_QUEST_START,
                GameConsts.HUD_QUEST_FINISH,
                GameConsts.HUD_QUEST_ABORT,
                GameConsts.HUD_QUEST_REWARD,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            QuestState state =(QuestState) notification.Body;
            switch (notification.Name)
            {
                case GameConsts.HUD_QUEST_START:
                    hudWindow.RefreshQuestUI(state);
                    break;
                case GameConsts.HUD_QUEST_FINISH:
                    hudWindow.RefreshQuestUI(state);
                    break;
                case GameConsts.HUD_QUEST_ABORT:
                    hudWindow.RefreshQuestUI(state);
                    break;
                case GameConsts.HUD_QUEST_REWARD:
                    hudWindow.RefreshQuestUI(state);
                    break;
            }
        }
    }
}