using CG.Game;
using CG.Ship.Modules;
using Gameplay.Enhancements;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Linq;
using VoidManager.Utilities;

namespace AutomaticShipSystems
{
    [HarmonyPatch(typeof(Enhancement))]
    internal class EnhancementPatch
    {
        [HarmonyPrepare]
        static void HookEvents()
        {
            Configs.TrimConfig.SettingChanged += ToggleAutomaticTrims;
        }

        [HarmonyPostfix]
        [HarmonyPatch("SetState")]
        static void SetState(Enhancement __instance, EnhancementState newState)
        {
            if (!PhotonNetwork.IsMasterClient || !VoidManagerPlugin.ModEnabled) return;

            if (Configs.TrimConfig.Value &&
                ClientGame.Current?.PlayerShip?.GetModule<Helm>()?.Engine?.GetComponentsInChildren<Enhancement>()?.Contains(__instance) == true &&
                newState == EnhancementState.Inactive)
            {
                Tools.DelayDoUnique(__instance, () => ResetTrim(__instance), Configs.TrimDelay.Value);
            }
        }

        private static void ResetTrim(Enhancement trim)
        {
            if (!PhotonNetwork.IsMasterClient || !VoidManagerPlugin.ModEnabled || !Game.PlayerShipExists) return;

            if (trim.CurrentState.Value == EnhancementState.Inactive)
            {
                trim.RequestStateChange(EnhancementState.Active, 1);
            }
        }

        internal static void ToggleAutomaticTrims(object sender, EventArgs e)
        {
            if (!PhotonNetwork.IsMasterClient || !VoidManagerPlugin.ModEnabled) return;

            Enhancement[] trims = ClientGame.Current?.PlayerShip?.GetModule<Helm>()?.Engine?.GetComponentsInChildren<Enhancement>();
            if (trims == null || trims.Length == 0) return;

            if (Configs.TrimConfig.Value)
            {
                foreach (Enhancement trim in trims)
                {
                    if (trim.CurrentState.Value == EnhancementState.Inactive)
                    {
                        Tools.DelayDoUnique(trim, () => ResetTrim(trim), Configs.TrimDelay.Value);
                    }
                }
            }
            else
            {
                foreach (Enhancement trim in trims)
                {
                    Tools.CancelDelayDoUnique(trim);
                }
            }
        }
    }
}
