using Mirror;
using UnityEngine;

namespace RoboBlast.Managers
{
    public class RoboBlastNetworkManager : NetworkManager
    {
        [SerializeField]
        private Transform _networkPlayerSpawn;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            var player = Instantiate(playerPrefab, _networkPlayerSpawn.position, _networkPlayerSpawn.rotation);

            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnStartClient()
        {
            UIManager.instance.EnableHUD();
        }

        public override void OnStopClient()
        {
            UIManager.instance.EnableMainMenu();
        }
    }
}