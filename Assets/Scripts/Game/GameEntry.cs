
using ECS;
using Game.Input;
using MVC;
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
            MainCamera.Init();
            // 针对URP对UI相机进行配置

            // 设置模糊相机，通常情况下在默认相机上层

            // 3D相机需要Brain，用来自动匹配相应的虚拟相机的位置

            // 设置需要显示3DUI上层的UI的相机mera3);

            // 缓存类型 
            ReferencePool.Instance.LoadTypes("MVC");
            ReferencePool.Instance.LoadTypes("ECS");
            ReferencePool.Instance.LoadTypes("Assembly-CSharp");
            //Facade.RegisterProxy(new PersistentDataProxy(GameConsts.RES_DESCRYPT_KEY));
            //Facade.RegisterProxy(new GlobalDataProxy());
            //Facade.RegisterProxy(new ConfigCenterProxy(loginServerType));
            SendNotification(RegistMediatorCommand.Name, this, StartupMediator.NAME);

            //AudioManager.Instance.CreateProvider<BgmProvider>(GameConsts.AUDIO_TAG_BGM, new BgmProvider.BgmParams { volume = 1 });
            //AudioManager.Instance.CreateProvider<SfxProvider>(GameConsts.AUDIO_TAG_SFX, new SfxProvider.SfxParams { volume = 0.8f, maxSourceCount = 8 });

            //PlayableDirectorManager.Instance.InitManager(30);
            //EntityManager.Instance.EnableDataMode(true);
            staticPanel.initEndCallback = OnStart;

            ResourceManager.Instance.LoadAssetAsync<TextAsset>("db", (asset, _) =>
            {
                Debug.LogError(asset.name);
            });
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
            GameInput.Enable();

            GameInput.Controller.Default.Escape.started += (ctx) =>
            {
                UIManager.Instance.CloseWindowFromStack();
            };
        }

        protected override void OnStop()
        {
            GameInput.Disable();
            //Facade.SendNotification(GameConsts.PERSISTENT_CLEAR_CACHE);
            //Facade.RemoveMediator(TCPMediator.NAME);
            //Facade.RemoveMediator(ILRuntimeMediator.NAME);

            //Facade.RemoveProxy(ProtoBufferProxy.NAME);
            //Facade.RemoveProxy(GlobalDataProxy.NAME);

            //Facade.RemoveProxy(TableProxy.NAME);
            base.OnStop();
        }

        protected override void InitializeCommand()
        {
            //Facade.RegisterCommand(SendTCPMsgCommand.NAME, () => new SendTCPMsgCommand());
            //Facade.RegisterCommand(CreateFollowCameraCommand.NAME, () => new CreateFollowCameraCommand());
            //Facade.RegisterCommand(BattleEndCommand.NAME, () => new BattleEndCommand());
        }

        protected override void InitializeProxy()
        {
            Facade.RegisterProxy(new TableProxy());
            //Facade.RegisterProxy(new ProtoBufferProxy(new C2S_Handler(), new LockStep_Handler()));
        }

        protected override void InitializeSystem()
        {
            WorldManager.Instance.Initialize();

            //SendNotification(ILRuntimeMediator.CMD_IL_GAME_START);


            ////logic system
            //World.Self.RegisterSystem<PathDrawSystem>();

            ////view system
            //World.Self.RegisterSystem<CreateBulletSystem>();
        }
    }
}
