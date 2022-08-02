using MVC;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Input
{
    public static class GameInput
    {
        //[DomainReload]
        public static GameControls Controller { get; private set; }

        public static void Enable()
        {
            if (Controller == null)
                Controller = new GameControls();

            Controller.Enable();
        }

        public static void Disable()
        {
            Controller.Disable();
        }

        public static bool EnhancedTouchEnabled => EnhancedTouchSupport.enabled;

        public static void EnableEnhancedTouch()
        {
            EnhancedTouchSupport.Enable();
        }

        public static void DisableEnhancedTouch()
        {
            EnhancedTouchSupport.Disable();
        }
    }
}
