using CG.Client.Ship.Interactions;
using CG.Game;
using CG.Ship.Modules;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using VoidManager.Utilities;

namespace AutomaticShipSystems
{
    [HarmonyPatch(typeof(ThrusterBooster))]
    internal class ThrusterBoosterPatch
    {
        private static readonly MethodInfo SetLeverPositionMethod = AccessTools.Method(typeof(Lever), "set_LeverPosition");

        [HarmonyPrepare]
        static void HookEvents()
        {
            Configs.ThrusterBoosterConfig.SettingChanged += ToggleAutomaticThrusterBoosters;
        }

        [HarmonyPostfix]
        [HarmonyPatch("ChangeState")]
        static void ChangeState(ThrusterBooster __instance, ThrusterBoosterState state)
        {
            if (!PhotonNetwork.IsMasterClient || !VoidManagerPlugin.ModEnabled) return;

            if (state == ThrusterBoosterState.Off && Configs.ThrusterBoosterConfig.Value)
            {
                Tools.DelayDoUnique(__instance, () => ChargeThrusterBooster(__instance), Configs.ThrusterBoosterDelay.Value);
            }
        }

        private static void ChargeThrusterBooster(ThrusterBooster booster)
        {
            if (!PhotonNetwork.IsMasterClient || !VoidManagerPlugin.ModEnabled || !Game.PlayerShipExists) return;

            if (booster.State == ThrusterBoosterState.Off)
            {
                SetLeverPositionMethod.Invoke(booster.ChargeLever, new object[] { 1f });
            }
        }

        internal static void ToggleAutomaticThrusterBoosters(object sender, EventArgs e)
        {
            if (!PhotonNetwork.IsMasterClient || !VoidManagerPlugin.ModEnabled) return;

            List<ThrusterBooster> boosters = ClientGame.Current.PlayerShip?.Transform?.GetComponent<ThrusterBoosterController>()?.ThrusterBoosters;
            if (boosters == null) return;

            if (Configs.ThrusterBoosterConfig.Value)
            {
                foreach (ThrusterBooster booster in boosters)
                {
                    if (booster.State == ThrusterBoosterState.Off)
                    {
                        Tools.DelayDoUnique(booster, () => ChargeThrusterBooster(booster), Configs.ThrusterBoosterDelay.Value);
                    }
                }
            }
            else
            {
                foreach (ThrusterBooster booster in boosters)
                {
                    Tools.CancelDelayDoUnique(booster);
                }
            }
        }
    }
}
