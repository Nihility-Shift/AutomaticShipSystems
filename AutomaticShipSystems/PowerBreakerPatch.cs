using CG.Game;
using Gameplay.PowerSystem;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using VoidManager.Utilities;

namespace AutomaticShipSystems
{
    [HarmonyPatch(typeof(PowerBreaker))]
    internal class PowerBreakerPatch
    {
        [HarmonyPrepare]
        static void HookEvents()
        {
            Configs.CircuitBreakerConfig.SettingChanged += ToggleAutomaticBreakers;
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnPowerStateChange")]
        static void OnPowerStateChange(PowerBreaker __instance, bool isOn)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            if (!isOn && Configs.CircuitBreakerConfig.Value)
            {
                Tools.DelayDoUnique(__instance, () => ResetCircuitBreakers(__instance), Configs.CircuitBreakerDelay);
            }
        }

        private static void ResetCircuitBreakers(PowerBreaker breaker)
        {
            if (!PhotonNetwork.IsMasterClient || !Tools.PlayerShipExists) return;

            breaker.IsOn.RequestChange(true);
        }

        internal static void ToggleAutomaticBreakers(object sender, EventArgs e)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            List<PowerBreaker> breakers = ClientGame.Current?.PlayerShip?.GetComponentInChildren<ProtectedPowerSystem>()?.Breakers;
            if (breakers == null) return;

            if (Configs.CircuitBreakerConfig.Value)
            {
                foreach (PowerBreaker powerBreaker in breakers)
                {
                    if (!powerBreaker.IsOn.Value)
                    {
                        Tools.DelayDoUnique(powerBreaker, () => powerBreaker.IsOn.RequestChange(true), Configs.CircuitBreakerDelay);
                    }
                }
            }
            else
            {
                foreach (PowerBreaker powerBreaker in breakers)
                {
                    Tools.CancelDelayDoUnique(powerBreaker);
                }
            }
        }
    }
}
