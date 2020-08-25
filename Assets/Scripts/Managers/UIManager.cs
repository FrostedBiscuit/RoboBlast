using UnityEngine;

namespace RoboBlast.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance = null;

        [SerializeField]
        private GameObject _mainMenu;
        [SerializeField]
        private GameObject _hud;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than 1 instance of UIManager in the scene!");

                return;
            }

            instance = this;

            _mainMenu?.SetActive(true);

            _hud?.SetActive(false);
        }

        public void EnableMainMenu()
        {
            _mainMenu?.SetActive(true);

            _hud?.SetActive(false);
        }

        public void EnableHUD()
        {
            _hud?.SetActive(true);

            _mainMenu?.SetActive(false);
        }
    }
}