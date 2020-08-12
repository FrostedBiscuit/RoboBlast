using System;
using UnityEngine;

namespace RoboBlast.Input.Interfaces
{
    public interface IMovementInput
    {
        event Action<Vector2> OnPlayerMoveInput;
    }
}
