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

        public override string Author => "18107";

        public override string Description => "Makes circuit breakers, thruster boosters, and trims self reset";
    }
}
