using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class StartupMediator : Mediator
    {
        public new static string NAME = typeof(StartupMediator).FullName;

        public const string RESOURCE_UPDATE = "RESOURCE_UPDATE";

        //private ConfigCenterProxy ccProxy;
        //private GlobalDataProxy globalProxy;

        private InitializePanel staticPanel;

        public StartupMediator(object viewComponent) : base(NAME, viewComponent)
        {
        }

        public override void OnRegister()
        {
            staticPanel = (InitializePanel)ViewComponent;

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            //globalProxy = Facade.RetrieveProxy(GlobalDataProxy.NAME) as GlobalDataProxy;

            staticPanel.splashEndCallback = () =>
            {
                //if (Facade.HasProxy(ConfigCenterProxy.NAME))
                //{
                //    ccProxy = Facade.RetrieveProxy(ConfigCenterProxy.NAME) as ConfigCenterProxy;
                //}
                //else
                //{
                //    ccProxy = new ConfigCenterProxy(globalProxy.loginServerType);
                //    Facade.RegisterProxy(ccProxy);
                //}

                //SendNotification(LoginSceneMediator.CMD_CAMERASHAKE_SIGNAL);
            };
        }

        public override void OnRemove()
        {
            //globalProxy = null;
            //ccProxy = null;
            staticPanel = null;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                //ConfigCenterProxy.CONFIG_CENTER_SUCCESS,
                //ConfigCenterProxy.CONFIG_CENTER_FAILURE,
                //TableProxy.TABLE_UPDATE_ONE,
                //TableProxy.TABLE_UPDATE_SUCCESS,
                //TableProxy.TABLE_UPDATE_FAILURE,
                GameConsts.CMD_CHECK_RESOURCE_UPDATE,
                GameConsts.CMD_SKIP_RESOURCE_UPDATE,
                GameConsts.CMD_START_RESOURCE_UPDATE,
                GameConsts.CMD_ALL_RESOURCE_UPDATED,
                GameConsts.CMD_GAME_START,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                //case ConfigCenterProxy.CONFIG_CENTER_SUCCESS:
                //    if (Facade.HasProxy(TableProxy.NAME))
                //    {
                //        var tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
                //        tableProxy.StartUpdate(0);
                //    }
                //    else
                //    {
                //        Facade.RegisterProxy(new TableProxy());
                //    }
                //    break;
               
                case GameConsts.CMD_GAME_START:
                    //globalProxy.LoadGameConstsEffectCfg();
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
