using System;

namespace RoboBlast.Input.Interfaces
{
    public interface IPlayerReadyInput
    {
        event Action<bool> OnReadyStatusUpdated;
    }
}