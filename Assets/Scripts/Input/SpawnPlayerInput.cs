using Mirror;
using RoboBlast.Input.Interfaces;
using RoboBlast.Managers;
using System;
using UnityEngine;

public class SpawnPlayerInput : MonoBehaviour, ISpawnPlayerInput
{
    public event Action SpawnPlayerCharacterInput;

    public void SpawnPlayer()
    {
        if (!NetworkClient.isConnected && RoboBlastNetworkManager.instance.BothPlayersReady)
            return;

        SpawnPlayerCharacterInput?.Invoke();
    }
}
