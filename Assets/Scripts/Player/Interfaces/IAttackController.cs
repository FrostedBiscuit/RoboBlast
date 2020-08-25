using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboBlast.Player.Interfaces
{
    public interface IAttackController
    {
        float DamageToDeal { get; }

        void Attack();
    }
}