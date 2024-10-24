using BepInEx.Configuration;

namespace AutomaticShipSystems
{
    internal class Configs
    {
        internal static ConfigEntry<bool> CircuitBreakerConfig;
        internal static ConfigEntry<bool> ThrusterBoosterConfig;
        internal static ConfigEntry<bool> TrimConfig;

        internal static ConfigEntry<double> CircuitBreakerDelay;
        internal static ConfigEntry<double> ThrusterBoosterDelay;
        internal static ConfigEntry<double> TrimDelay;

        internal static void Load(BepinPlugin plugin)
        {
            CircuitBreakerConfig = plugin.Config.Bind("AutomaticShipSystems", "CircuitBreakers", true);
            ThrusterBoosterConfig = plugin.Config.Bind("AutomaticShipSystems", "ThrusterBooster", true);
            TrimConfig = plugin.Config.Bind("AutomaticShipSystems", "Trim", true);

            CircuitBreakerDelay = plugin.Config.Bind("AutomaticShipSystems", "CircuitBreakersTimer", (double)(30 * 1000));
            ThrusterBoosterDelay = plugin.Config.Bind("AutomaticShipSystems", "ThrusterBoosterTimer", (double)(60 * 1000));
            TrimDelay = plugin.Config.Bind("AutomaticShipSystems", "TrimTimer", (double)(5 * 60 * 1000));
        }
    }
}
