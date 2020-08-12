using System;

namespace RoboBlast.Player.Interfaces
{
    public interface IHealthController
    {
        event Action OnDeath;

        float CurrentHealth { get; }
        float MaxHealth { get; }

        void TakeDamage(float amount);
    }
}
