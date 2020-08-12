using RoboBlast.Input.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboBlast.Input
{
    public class AttackInput : MonoBehaviour, IAttackInput
    {
        public event Action OnPlayerPrimaryAttack;
        public event Action OnPlayerAltAttack;

        public void PrimaryAttack()
        {
            OnPlayerPrimaryAttack?.Invoke();
        }

        public void AltAttack()
        {
            OnPlayerAltAttack?.Invoke();
        }
    }
}