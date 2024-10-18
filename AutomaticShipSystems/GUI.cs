using VoidManager.CustomGUI;
using VoidManager.Utilities;

namespace AutomaticShipSystems
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name() => MyPluginInfo.USERS_PLUGIN_NAME;

        public override void Draw()
        {
            GUITools.DrawCheckbox("Circuit Breakers", ref Configs.CircuitBreakerConfig);
            GUITools.DrawCheckbox("Thruster Boosters", ref Configs.ThrusterBoosterConfig);
            GUITools.DrawCheckbox("Trims", ref Configs.TrimConfig);
        }
    }
}
