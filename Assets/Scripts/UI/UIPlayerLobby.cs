using TMPro;
using UnityEngine;

namespace RoboBlast.UI
{
    public class UIPlayerLobby : MonoBehaviour
    {
        [System.Serializable]
        public class UIPlayerLabel
        {
            [SerializeField]
            private TextMeshProUGUI _name;
            [SerializeField]
            private TextMeshProUGUI _readyStatus;

            public string Name 
            { 
                set 
                {
                    _name.text = value;
                } 
            }

            public bool ReadyStatus
            {
                set
                {
                    _readyStatus.text = value ? "Ready" : "Not ready";
                }
            }
        }

        [SerializeField]
        private UIPlayerLabel _localPlayerLabel;
        [SerializeField]
        private UIPlayerLabel _remotePlayerLabel;

        public UIPlayerLabel LocalPlayerLabel
        {
            get
            {
                return _localPlayerLabel;
            }
        }

        public UIPlayerLabel RemotePlayerLabel
        {
            get
            {
                return _remotePlayerLabel;
            }
        }
    }
}