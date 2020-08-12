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

        public event Action OnDeath;

        [SerializeField]
        private float _maxHealth = 100f;

        [SyncVar]
        private float _currentHealth;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            //CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0f, float.MaxValue);

            CmdTakeDamage(amount);

            if (CurrentHealth == 0f)
                OnDeath?.Invoke();
        }

        [Command]
        public void CmdTakeDamage(float amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0f, float.MaxValue);

            Debug.Log($"Health going down {amount}");
            //if (CurrentHealth == 0f)
            //    OnDeath?.Invoke();
        }
    }
}
