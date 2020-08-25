using Mirror;
using RoboBlast.Input.Interfaces;
using RoboBlast.Repositories;
using RoboBlast.UI;
using RoboBlast.UI.Interfaces;
using UnityEngine;

namespace RoboBlast.Player
{
    public class PlayerEntity : NetworkBehaviour
    {
        //[HideInInspector]
        [SyncVar(hook = "playerNameUpdated")]
        public string Name;

        [SerializeField]
        private GameObject _playerPrefab;

        private ISpawnPlayerInput _spawnInput;

        private void Start()
        {
            if (!hasAuthority)
                return;

            CmdSetName(PlayerNameRepository.PlayerName);

            _spawnInput = FindObjectOfType<SpawnPlayerInput>();
            _spawnInput.SpawnPlayerCharacterInput += CmdSpawnPlayer;
        }

        [Command]
        public void CmdSpawnPlayer()
        {
            var spawn = NetworkManager.singleton.GetStartPosition();
            var player = Instantiate(_playerPrefab, spawn.position, spawn.rotation);

            Debug.Log($"Spawning {Name}'s character");

            NetworkServer.Spawn(player, gameObject);
        }

        private void OnDisable()
        {
            if (!hasAuthority)
                return;

            _spawnInput.SpawnPlayerCharacterInput -= CmdSpawnPlayer;
        }

        [Command]
        public void CmdSetName(string name)
        {
            Name = name;
        }

        private void playerNameUpdated(string oldValue, string newValue)
        {
            GetComponent<UILobbyPlayer>().UpdateLabel();
        }
    }
}