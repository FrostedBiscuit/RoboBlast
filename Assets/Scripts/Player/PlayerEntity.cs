using Mirror;
using RoboBlast.Input;
using RoboBlast.Input.Interfaces;
using RoboBlast.Managers;
using RoboBlast.Repositories;
using System;
using UnityEngine;

namespace RoboBlast.Player
{
    public class PlayerEntity : NetworkBehaviour
    {
        public event Action<string, bool> OnNameChanged;
        public event Action<bool, bool> OnReadyStatusChanged;

        //[HideInInspector]
        [SyncVar(hook = "playerNameUpdated")]
        public string Name;

        [SyncVar(hook = "playerReadyStatusUpdated")]
        public bool Ready = false;

        [SerializeField]
        private GameObject _playerPrefab;

        private ISpawnPlayerInput _spawnInput;
        private IPlayerReadyInput _readyInput;

        private void Start()
        {
            if (!hasAuthority)
                return;

            CmdSetName(PlayerNameRepository.PlayerName);

            _spawnInput = FindObjectOfType<SpawnPlayerInput>();
            _readyInput = FindObjectOfType<PlayerReadyInput>();

            registerFromUIEvents();
        }

        [Command]
        public void CmdSpawnPlayer()
        {
            var spawn = NetworkManager.singleton.GetStartPosition();
            var player = Instantiate(_playerPrefab, spawn.position, spawn.rotation);

            Debug.Log($"Spawning {Name}'s character");

            NetworkServer.Spawn(player, gameObject);
        }

        private void OnDestroy()
        {
            if (hasAuthority)
            {
                unregisterFromUIEvents();
            }
        }

        [Command]
        public void CmdSetName(string name)
        {
            Name = name;
        }

        [Command]
        public void CmdUpdateReadyStatus(bool ready)
        {
            Ready = ready;
        }
        
        private void playerNameUpdated(string oldValue, string newValue)
        {
            OnNameChanged?.Invoke(newValue, hasAuthority);
        }

        private void playerReadyStatusUpdated(bool oldValue, bool newValue)
        {
            OnReadyStatusChanged?.Invoke(Ready, hasAuthority);

            RoboBlastNetworkManager.instance.SetPlayerReady(this);
        }

        public override void OnStopServer()
        {
            unregisterFromUIEvents();
        }

        public override void OnStopClient()
        {
            unregisterFromUIEvents();

            base.OnStopClient();
        }

        private void registerFromUIEvents()
        {
            if (hasAuthority)
            {
                _spawnInput.SpawnPlayerCharacterInput += CmdSpawnPlayer;
                _readyInput.OnReadyStatusUpdated += CmdUpdateReadyStatus;
            }
        }

        private void unregisterFromUIEvents()
        {
            if (hasAuthority)
            {
                _spawnInput.SpawnPlayerCharacterInput -= CmdSpawnPlayer;
                _readyInput.OnReadyStatusUpdated -= CmdUpdateReadyStatus;
            }
        }
    }
}