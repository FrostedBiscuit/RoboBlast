using System;

namespace RoboBlast.Input.Interfaces
{
    public interface IAttackInput
    {
        event Action OnPlayerPrimaryAttack;
        event Action OnPlayerAltAttack;
    }
}