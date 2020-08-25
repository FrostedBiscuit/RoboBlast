using Mirror;
using RoboBlast.Input.Interfaces;
using System;
using UnityEngine;

public class SpawnPlayerInput : MonoBehaviour, ISpawnPlayerInput
{
    public event Action SpawnPlayerCharacterInput;

    public void SpawnPlayer()
    {
        if (!NetworkClient.isConnected)
            return;

        SpawnPlayerCharacterInput?.Invoke();
    }
}
