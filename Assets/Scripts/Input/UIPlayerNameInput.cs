using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RoboBlast.Repositories;
using Mirror;

namespace RoboBlast.Input
{
    public class UIPlayerNameInput : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _nameInput;
        [SerializeField]
        private Button _submitButton;

        private void Start()
        {
            if (PlayerPrefs.HasKey(PlayerNameRepository.PlayerNameKey))
                _nameInput.text = PlayerNameRepository.PlayerName;
        }

        public void UpdatePlayerName()
        {
            PlayerNameRepository.PlayerName = _nameInput.text;
        }

        public void ValidateInput(string input)
        {
            _submitButton.enabled = input.Length > 0;
        }
    }
}