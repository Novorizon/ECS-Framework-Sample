using DataBase;
using MVC;
using MVC.Providers;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Game
{
    public class StartupMediator : Mediator
    {
        public new static string NAME = typeof(StartupMediator).FullName;

        public const string RESOURCE_UPDATE = "RESOURCE_UPDATE";

        private TableProxy tableProxy;

        private InitializePanel staticPanel;

        public StartupMediator(object viewComponent) : base(NAME, viewComponent)
        {
        }

        public override void OnRegister()
        {
            staticPanel = (InitializePanel)ViewComponent;

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            SendNotification(LoadDatabaseCommand.NAME, "db");


            AddressableProvider provider = new AddressableProvider();
            //provider.InitializedCallback = InitializeILRuntime;
            ResourceManager.Instance.Initialize(provider);


            SendNotification(RegisterTableCommand.NAME);
        }

        public override void OnRemove()
        {
            staticPanel = null;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                GameConsts.REGISTER_TABLE,
                GameConsts.LOAD_DB,
                GameConsts.LOAD_DB_FINISH,

                GameConsts.LOAD_SCENE_FINISH,

                GameConsts.CMD_GAME_START,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case GameConsts.LOAD_DB:
                    tableProxy.Load();

                    break;

                case GameConsts.LOAD_DB_FINISH:
                    LoadSceneData data = new LoadSceneData("map_1001", LoadSceneMode.Additive);
                    SendNotification(LoadSceneCommand.NAME, data);
                    break;

                case GameConsts.LOAD_SCENE_FINISH:
                    break;
                case GameConsts.CMD_GAME_START:
                    staticPanel.OnInitializedEnd();
                    break;
            }
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.SetActiveScene(arg0);
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void UpdateProgress(string tips, float progress)
        {
            if (staticPanel != null)
            {
                staticPanel.SetProgress(tips, progress);
            }
        }

        private void ShowErrorPopWindow(string errorMsg, string btnOk, string btnCancel)
        {
            if (staticPanel != null)
            {
                staticPanel.Pop(errorMsg, () =>
                {
                    //ccProxy.StartConnect();
                },
                btnOk, () =>
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }, btnCancel);
            }
        }



    }
}
