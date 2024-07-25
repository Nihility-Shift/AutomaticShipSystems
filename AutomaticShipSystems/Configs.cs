using BepInEx.Configuration;

namespace AutomaticShipSystems
{
    internal class Configs
    {
        internal const double CircuitBreakerDelay = 30 * 1000;
        internal const double ThrusterBoosterDelay = 60 * 1000;
        internal const double TrimDelay = 5 * 60 * 1000;

        internal static ConfigEntry<bool> CircuitBreakerConfig;
        internal static ConfigEntry<bool> ThrusterBoosterConfig;
        internal static ConfigEntry<bool> TrimConfig;

        internal static void Load(BepinPlugin plugin)
        {
            CircuitBreakerConfig = plugin.Config.Bind("AutomaticShipSystems", "CircuitBreakers", true);
            ThrusterBoosterConfig = plugin.Config.Bind("AutomaticShipSystems", "ThrusterBooster", true);
            TrimConfig = plugin.Config.Bind("AutomaticShipSystems", "Trim", true);
        }
    }
}
