
using ECS;
using Game.Input;
using MVC;
using MVC.Providers;
using MVC.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameEntry : ApplicationEntry
    {
        public InitializePanel staticPanel;

        protected override void OnLaunch()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

#if UNITY_EDITOR
            GameObjectPool.Instance.ExpiredTime = 60;
#else
            GameObjectPool.Instance.ExpiredTime = 180;
#endif
            GameInput.Enable();
            MainCamera.Init();
            UIManager.Instance.GetCamera(0);
            // 缓存类型 
            ReferencePool.Instance.LoadTypes("MVC");
            ReferencePool.Instance.LoadTypes("ECS");
            ReferencePool.Instance.LoadTypes("Assembly-CSharp");
            staticPanel.initEndCallback = OnStart;


            Facade.RegisterProxy(new TableProxy());
            Facade.RegisterCommand(RegisterTableCommand.NAME, () => new RegisterTableCommand());
            Facade.RegisterCommand(LoadSceneCommand.NAME, () => new LoadSceneCommand());

            SendNotification(RegistMediatorCommand.Name, staticPanel, StartupMediator.NAME);

        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.SetActiveScene(arg0);
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public override void OnStart()
        {
            base.OnStart();

            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);
            

            GameInput.Controller.Default.Escape.started += (ctx) =>
            {
                UIManager.Instance.CloseWindowFromStack();
            };


            SendNotification(LoadHeroCommand.NAME);

        }

        protected override void OnStop()
        {
            GameInput.Disable();
            //Facade.SendNotification(GameConsts.PERSISTENT_CLEAR_CACHE);

            //Facade.RemoveMediator(StartupMediator.NAME);

            Facade.RemoveProxy(TableProxy.NAME);

            base.OnStop();
        }

        protected override void InitializeCommand()
        {
            Facade.RegisterCommand(LoadSceneCommand.NAME, () => new LoadSceneCommand());
            Facade.RegisterCommand(LoadHeroCommand.NAME, () => new LoadHeroCommand());
        }

        protected override void InitializeProxy()
        {
            Facade.RegisterProxy(new HeroProxy());
        }

        protected override void InitializeSystem()
        {
            WorldManager.Instance.Initialize();

            //SendNotification(ILRuntimeMediator.CMD_IL_GAME_START);
            World.Self.RegisterSystem<ControllerMoveSystem>();
            World.Self.RegisterSystem<ControllerLookAtSystem>();
            World.Self.RegisterSystem<FollowCameraRollSystem>();
            World.Self.RegisterSystem<FaceForwardSystem>();
            World.Self.RegisterSystem<PlayerMoveSystem>();
            World.Self.RegisterSystem<TRSToLocalToWorldSystem>();
            World.Self.RegisterSystem<CopyToTransformSystem>();
            //World.Self.RegisterSystem<CopyFromTransformSystem>();

            ////logic system
            //World.Self.RegisterSystem<PathDrawSystem>();

            ////view system
            //World.Self.RegisterSystem<CreateBulletSystem>();
        }
    }
}
