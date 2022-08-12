using MVC.UI;
using static MVC.UI.UIWindow;

namespace Game
{
    public static class UIConfig
    {
        public static UIWindowParams HUD = new UIWindowParams
        {
            name = "HUD",
            prefabPath = "HUD",
            layer = UILayer.Top,
            orderInLayer = 999,
            windowMode = WindowMode.Single,
            windowClass = typeof(HudWindow).FullName,
            mediatorName = HudMediator.NAME,
        };

        public static UIWindowParams DialogueWindow = new UIWindowParams
        {
            name = "Dialogue",
            prefabPath = "Dialogue",
            layer = UILayer.Top,
            orderInLayer = 999,
            windowMode = WindowMode.Single,
            windowClass = typeof(DialogueWindow).FullName,
            mediatorName = DialogueMediator.NAME,
        };

        public static UIWindowParams GMWindow = new UIWindowParams
        {
            name = "GMWindow",
            layer = MVC.UI.UILayer.Top,
            orderInLayer = 999,
            windowMode = WindowMode.SingleInStack,
            prefabPath = "Management/GMWindow.prefab",
            windowClass = typeof(UIWindow).FullName,
        };
    }
}
