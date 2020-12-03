using RoboBlast.Player;
using UnityEngine;

namespace RoboBlast.UI
{
    [RequireComponent(typeof(PlayerEntity))]
    public class UILobbyPlayer : MonoBehaviour
    {
        private PlayerEntity _player;
        private UIPlayerLobby _playerLobby;

        private void Start()
        {
            _player = GetComponent<PlayerEntity>();
            _playerLobby = FindObjectOfType<UIPlayerLobby>();

            updateName(_player.Name, _player.hasAuthority);
            updateReadyStatus(_player.Ready, _player.hasAuthority);

            _player.OnNameChanged += updateName;
            _player.OnReadyStatusChanged += updateReadyStatus;
        }

        private void updateName(string name, bool localPlayer)
        {
            if (localPlayer)
                _playerLobby.LocalPlayerLabel.Name = name;
            else
                _playerLobby.RemotePlayerLabel.Name = name;
        }

        private void updateReadyStatus(bool ready, bool localPlayer)
        {
            if (localPlayer)
                _playerLobby.LocalPlayerLabel.ReadyStatus = ready;
            else
                _playerLobby.RemotePlayerLabel.ReadyStatus = ready;
        }

        private void OnDestroy()
        {
            updateName("Waiting for player...", _player.hasAuthority);
            updateReadyStatus(false, _player.hasAuthority);

            _player.OnNameChanged -= updateName;
            _player.OnReadyStatusChanged -= updateReadyStatus;
        }
    }
}