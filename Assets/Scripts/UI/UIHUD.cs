using UnityEngine;

namespace RoboBlast.UI
{
    public class UIHUD : MonoBehaviour
    {
        [SerializeField]
        private GameObject _lobbyHUD;
        [SerializeField]
        private GameObject _gameHUD;

        // Start is called before the first frame update
        private void Start()
        {
            _disableGameHUDOnLoad();
        }

        private void OnEnable()
        {
            _disableGameHUDOnLoad();
        }

        private void _disableGameHUDOnLoad()
        {
            _gameHUD?.SetActive(false);
            _lobbyHUD?.SetActive(true);
        }
    }
}