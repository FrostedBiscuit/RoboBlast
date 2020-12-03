using Mirror;
using RoboBlast.Managers;
using UnityEngine;

namespace RoboBlast.UI.Buttons
{
    public class UIExitButton : MonoBehaviour
    {
        public void ExitGame()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                RoboBlastNetworkManager.instance.StopHost();
            }
            else if (NetworkClient.isConnected)
            {
                RoboBlastNetworkManager.instance.StopClient();
            }
            else if (NetworkServer.active)
            {
                RoboBlastNetworkManager.instance.StopServer();
            }
        }
    }
}
