using Mirror;
using RoboBlast.Player.Interfaces;
using System;
using UnityEngine;

namespace RoboBlast.Player
{
    public class PlayerHealthController : NetworkBehaviour, IHealthController
    {
        public float CurrentHealth 
        { 
            get
            {
                return _currentHealth;
            } 
            protected set
            {
                _currentHealth = value;
            } 
        }

        public float MaxHealth => _maxHealth;

        [SyncEvent]
        public event Action OnDeath;

        [SerializeField]
        private float _maxHealth = 100f;

        [SyncVar]
        private float _currentHealth;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        [Server]
        public void TakeDamage(float amount)
        {
            if (!isServer)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0f, float.MaxValue);

            if (CurrentHealth == 0f)
                OnDeath?.Invoke();

            Debug.Log($"Current health {CurrentHealth}");
        }
    }
}
