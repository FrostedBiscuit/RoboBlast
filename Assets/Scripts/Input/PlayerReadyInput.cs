using RoboBlast.Input.Interfaces;
using System;
using UnityEngine;

namespace RoboBlast.Input
{
    public class PlayerReadyInput : MonoBehaviour, IPlayerReadyInput
    {
        public event Action<bool> OnReadyStatusUpdated;

        public bool Ready { get; private set; } = false;

        public void UpdateReadyStatus()
        {
            Ready = !Ready;

            //Debug.Log($"Ready status: {Ready}");

            OnReadyStatusUpdated?.Invoke(Ready);
        }
    }
}