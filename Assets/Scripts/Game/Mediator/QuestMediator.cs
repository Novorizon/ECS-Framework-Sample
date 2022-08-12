using DataBase;
using ECS;
using Game;
using MVC.UI;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace Game
{
    public class QuestMediator : Mediator
    {
        new public static string NAME = typeof(QuestMediator).FullName;

        private QuestWindow window;

        public QuestMediator(object viewComponent) : base(NAME, viewComponent) { }

        public override void OnRegister()
        {
            base.OnRegister();
            window = ViewComponent as QuestWindow;
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
                case GameConsts.QUEST_SELECT:
                    //判断距离 在交互距离内 弹出对话框
                    Entity entity = (Entity)notification.Body;
                    HeroProxy proxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
                    if (proxy == null)
                        return;
                    Vector3 position = proxy.GetData().position;
                    float d = (position - entity.gameObject.transform.position).magnitude;
                    if (d < 100)
                    {
                        UIManager.Instance.OpenWindow(UIConfig.DialogueWindow, entity);
                    }
                    break;
                case GameConsts.QUEST_START:
                    int id = 1;
                    //生成任务entity 用于任务系统更新状态
                    Entity questEntity = EntityManager.Create();
                    Quest quest = EntityManager.Instance.AddComponentData<Quest>(questEntity);
                    quest.id = id;
                    quest.state = QuestState.Start;
                    quest.stage = 0;

                    //查询任务完成条件
                    TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
                    if (tableProxy == null)
                        return;

                    QuestData data = tableProxy.GetData<QuestData>(id);
                    quest.items = new System.Collections.Generic.List<ItemUnit>();
                    for (int i = 0; i < data.items.Count; i++)
                    {
                        ItemUnit item = data.items[i];
                        quest.items.Add(item);
                    }

                    //任务proxy更新新的任务
                    QuestProxy questProxy = Facade.RetrieveProxy(QuestProxy.NAME) as QuestProxy;
                    if (questProxy == null)
                        return;

                    QuestVO vo = new QuestVO();
                    vo.id = id;
                    vo.state = QuestState.Start;
                    vo.stage = 0;
                    questProxy.SetData(vo);


                    break;
            }
        }
    }
}