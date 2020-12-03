using Mirror;
using RoboBlast.Player;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace RoboBlast.Managers
{
    public class RoboBlastNetworkManager : NetworkManager
    {
        #region Singelton
        public static RoboBlastNetworkManager instance;

        new private void Awake()
        {
            if (instance != null)
                Debug.LogError("More than 1 instance of RoboBlastNetworkManager in the scene!!!");

            instance = this;
        }
        #endregion

        public bool BothPlayersReady 
        {
            get
            {
                if (_playerEntities.Count < 2)
                    return false;

                return _playerEntities.All(p => p.Ready);
            }
        }

        private List<PlayerEntity> _playerEntities = new List<PlayerEntity>();

        private List<PlayerController> _players = new List<PlayerController>();

        [SerializeField]
        private Transform _networkPlayerSpawn;

        private void Update()
        {
            if (BothPlayersReady == true)
            {
                foreach (KeyValuePair<uint, NetworkIdentity> spawned in NetworkIdentity.spawned)
                {
                    var playerCharacter = spawned.Value.GetComponent<PlayerController>();

                    if (playerCharacter != null && _players.Contains(playerCharacter) == false)
                    {
                        _players.Add(playerCharacter);
                    }
                }
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            var player = Instantiate(playerPrefab, _networkPlayerSpawn.position, _networkPlayerSpawn.rotation).GetComponent<PlayerEntity>();

            _playerEntities.Add(player);

            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            cleanPlayerEntities();

            var disconnectingPlayer = _playerEntities.SingleOrDefault(p => p != null && p.netIdentity.connectionToServer == conn); 

            if (disconnectingPlayer != null)
            {
                _playerEntities.Remove(disconnectingPlayer);
            }

            base.OnServerDisconnect(conn);
        }

        public override void OnStartClient()
        {
            UIManager.instance.EnableHUD();
        }

        public override void OnStopClient()
        {
            _playerEntities.Clear();

            UIManager.instance.EnableMainMenu();
        }

        public void SetPlayerReady(PlayerEntity player)
        {
            if (_playerEntities.Contains(player))
            {
                _playerEntities[_playerEntities.IndexOf(player)].Ready = player.Ready;

                return;
            }

            _playerEntities.Add(player);
        }

        private void cleanPlayerEntities()
        {
            //Debug.Log($"Player entities count: {_playerEntities.Count}");

            for (int i = _playerEntities.Count - 1; i >= 0; i--)
            {
                if (_playerEntities[i] == null)
                {
                    _playerEntities.RemoveAt(i);
                }
            }
        }
    }
}