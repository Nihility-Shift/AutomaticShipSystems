using Photon.Pun;
using VoidManager;
using VoidManager.MPModChecks;

namespace AutomaticShipSystems
{
    public class VoidManagerPlugin : VoidPlugin
    {
        public VoidManagerPlugin()
        {
            Events.Instance.MasterClientSwitched += (_, _) => {
                if (PhotonNetwork.IsMasterClient)
                {
                    PowerBreakerPatch.ToggleAutomaticBreakers(null, null);
                    ThrusterBoosterPatch.ToggleAutomaticThrusterBoosters(null, null);
                    EnhancementPatch.ToggleAutomaticTrims(null, null);
                } };
        }

        public override MultiplayerType MPType => MultiplayerType.Host;

        public override string Author => MyPluginInfo.PLUGIN_AUTHORS;

        public override string Description => MyPluginInfo.PLUGIN_DESCRIPTION;

        public override string ThunderstoreID => MyPluginInfo.PLUGIN_THUNDERSTORE_ID;

        public static bool ModEnabled = false;

        internal static void Enable()
        {
            ModEnabled = true;
            VoidManager.Progression.ProgressionHandler.DisableProgression(MyPluginInfo.PLUGIN_GUID);
        }

        public override SessionChangedReturn OnSessionChange(SessionChangedInput input)
        {
            switch(input.CallType)
            {
                case CallType.Joining:
                    ModEnabled = false;
                    break;
                case CallType.SessionEscalated:
                case CallType.HostStartSession:
                    Enable();
                    return new SessionChangedReturn() { SetMod_Session = true};
            }
            return base.OnSessionChange(input);
        }
    }
}
