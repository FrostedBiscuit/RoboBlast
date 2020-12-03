using RoboBlast.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace RoboBlast.UI.Buttons
{
    public class UISpawnPlayerButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Start()
        {
            _checkForButtonInteractivity();
        }

        private void OnEnable()
        {
            _checkForButtonInteractivity();
        }

        private void Update()
        {
            _checkForButtonInteractivity();
        }

        private void _checkForButtonInteractivity()
        {
            _button.interactable = RoboBlastNetworkManager.instance.BothPlayersReady;
        }
    }
}