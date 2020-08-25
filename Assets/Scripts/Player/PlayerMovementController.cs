using UnityEngine;
using Mirror;
using RoboBlast.Player.Interfaces;

namespace RoboBlast.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : NetworkBehaviour, IMovementController
    {
        [SerializeField]
        private float _speed = 10f;

        private Rigidbody _rigidbody;
        private Transform _transfrom;

        // Use this for initialization
        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transfrom = transform;
        }

        public void Move(Vector2 input)
        {
            var direction = new Vector3(input.x, 0f, input.y) * _speed * Time.deltaTime;

            _rigidbody.MovePosition(direction + _transfrom.position);

            CmdMove(direction);
        }

        [Command]
        public void CmdMove(Vector3 amount)
        {
            _rigidbody.MovePosition(amount + _transfrom.position);
        }
    }
}