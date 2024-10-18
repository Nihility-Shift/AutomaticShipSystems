using Photon.Pun;
using VoidManager.MPModChecks;

namespace AutomaticShipSystems
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public VoidManagerPlugin()
        {
            VoidManager.Events.Instance.MasterClientSwitched += (_, _) => {
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
    }
}
