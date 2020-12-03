using RoboBlast.Constants;
using RoboBlast.Player.Interfaces;
using UnityEngine;

namespace RoboBlast.Player.Util
{
    [RequireComponent(typeof(Animator))]
    public class AttackEventListener : MonoBehaviour
    {
        private IAttackController _attackController = null;
        
        private Animator _animator = null;

        private void Start()
        {
            _attackController = transform.parent.GetComponent<IAttackController>();

            _animator = GetComponent<Animator>();
        }

        public void PrimaryAttack()
        {
            Debug.Log("Attack listener here!!!");

            _attackController.Attack();

            _animator.ResetTrigger(RoboBlastConsts.Animation.AttackTriggerKey);
        }
    }
}