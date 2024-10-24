using UnityEngine;
using VoidManager.CustomGUI;
using VoidManager.Utilities;

namespace AutomaticShipSystems
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name() => MyPluginInfo.USERS_PLUGIN_NAME;

        public override void Draw()
        {
            GUILayout.Label("Timers in MS. 1000 is equal to 1 second.\n");

            GUITools.DrawCheckbox("Auto Circuit Breakers Enabled", ref Configs.CircuitBreakerConfig);
            GUITools.DrawTextField("Circut Breaker Timer", ref Configs.CircuitBreakerDelay);
            GUILayout.Label(string.Empty);
            GUITools.DrawCheckbox("Auto Thruster Boosters Enabled", ref Configs.ThrusterBoosterConfig);
            GUITools.DrawTextField("Thruster Booster Timer", ref Configs.ThrusterBoosterDelay);
            GUILayout.Label(string.Empty);
            GUITools.DrawCheckbox("Auto Trims Enabled", ref Configs.TrimConfig);
            GUITools.DrawTextField("Trim Timer", ref Configs.TrimDelay);
        }
    }
}
