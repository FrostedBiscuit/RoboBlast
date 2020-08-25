using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

namespace RoboBlast.Input
{
    public class UIMainMenuButtons : MonoBehaviour
    {
        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private Button _serverButton;

        [SerializeField]
        private TMP_InputField _ipInput;

        private NetworkManager _networkManager;

        private void Awake()
        {
            _networkManager = NetworkManager.singleton;
        }

        public void Host()
        {
            _networkManager.StartHost();
        }

        public void Connect()
        {
            _networkManager.networkAddress = _ipInput.text;
            _networkManager.StartClient();
        }

        public void Server()
        {
            _networkManager.StartServer();
        }

        public void ValidateIpInput(string input)
        {
            _connectButton.enabled = input.Length > 0;
        }
    }
}