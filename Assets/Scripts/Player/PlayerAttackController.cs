using Mirror;
using RoboBlast.Player.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoboBlast.Player
{
    public class PlayerAttackController : NetworkBehaviour, IAttackController
    {
        public float DamageToDeal => 50f;

        [SerializeField]
        private Transform _attackPointTransform;

        [SerializeField]
        private float _areaOfEffectRadius = 0.3f;

        public void Attack()
        {
            CmdAttack();
        }

        [Command]
        public void CmdAttack()
        {
            Debug.Log("Attacking on the server! :D");

            var hits = Physics.OverlapSphere(_attackPointTransform.position, _areaOfEffectRadius);
            var opponentHealthController = hits.Select(h => h.GetComponent<IHealthController>()).Where(hc => hc != null).FirstOrDefault();

            if (opponentHealthController == null)
                return;

            opponentHealthController.TakeDamage(DamageToDeal);
        }

        //public void AssignAuthority(NetworkConnection connection)
        //{
        //    netIdentity.AssignClientAuthority(connection);
        //}
    }
}
