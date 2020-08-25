using Mirror;
using RoboBlast.Player;
using UnityEngine;

namespace RoboBlast.UI
{
    [RequireComponent(typeof(PlayerEntity))]
    public class UILobbyPlayer : MonoBehaviour
    {
        private const string ReadyText = "Ready";
        private const string NotReadyText = "Not ready";

        private PlayerEntity _player;

        public void UpdateLabel()
        {
            _player = GetComponent<PlayerEntity>();

            var playerLobby = FindObjectOfType<UIPlayerLobby>();

            if (_player.netIdentity.hasAuthority)
                playerLobby.LocalPlayerLabel.Name = _player.Name;
            else
                playerLobby.LocalPlayerLabel.Name = _player.Name;
        }
    }
}