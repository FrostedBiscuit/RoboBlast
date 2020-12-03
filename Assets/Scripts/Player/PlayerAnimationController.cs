using Mirror;
using RoboBlast.Constants;
using RoboBlast.Input;
using RoboBlast.Input.Interfaces;
using UnityEngine;

namespace RoboBlast.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        /// <summary>
        /// The Dot product threshold at which the character 
        /// is concidered to be stationary.
        /// </summary>
        private const float IdleThreshold = 0.02f;

        private Animator _animator;

        private IAttackInput _playerAttackInput;

        public NetworkAnimator NetworkAnimator = null;

        private void Start()
        {
            _animator = NetworkAnimator.animator;

            _playerAttackInput = FindObjectOfType<AttackInput>();

            _playerAttackInput.OnPlayerPrimaryAttack += onPrimaryAttackInput;

            _lastPosition = transform.position;
        }

        private void Update()
        {
            updateMovementValues();
        }

        private void OnDestroy()
        {
            if (!enabled)
                return;
                
            _playerAttackInput.OnPlayerPrimaryAttack -= onPrimaryAttackInput;
        }

        private void onPrimaryAttackInput()
        {
            Debug.Log("Attacking!");

            _animator.SetTrigger(RoboBlastConsts.Animation.AttackTriggerKey);
            NetworkAnimator.SetTrigger(RoboBlastConsts.Animation.AttackTriggerKey);
        }

        private Vector3 _lastPosition;

        private void updateMovementValues()
        {
            var forwardDot = 0f;
            var rightDot = 0f;

            if (transform.position != _lastPosition)
            {
                var dir = transform.position - _lastPosition;

                forwardDot = Vector3.Dot(transform.forward, dir);
                rightDot = Vector3.Dot(transform.right, dir);
            }

            var desiredXVelocity = getDesiredVelocity(rightDot);
            var desiredYVelocity = getDesiredVelocity(forwardDot);

            var previousXVelocity = _animator.GetFloat(RoboBlastConsts.Animation.VelocityXKey);
            var previousYVelocity = _animator.GetFloat(RoboBlastConsts.Animation.VelocityYKey);

            _animator.SetFloat(RoboBlastConsts.Animation.VelocityXKey, Mathf.Lerp(previousXVelocity, desiredXVelocity, Time.deltaTime * 5f));
            _animator.SetFloat(RoboBlastConsts.Animation.VelocityYKey, Mathf.Lerp(previousYVelocity, desiredYVelocity, Time.deltaTime * 5f));

            _lastPosition = transform.position;
        }

        private float getDesiredVelocity(float dot)
        {
            if (dot > IdleThreshold)
            {
                return 2f;
            }
            else if (dot < -IdleThreshold)
            {
                return -2f;
            }

            return 0f;
        }
    }
}
