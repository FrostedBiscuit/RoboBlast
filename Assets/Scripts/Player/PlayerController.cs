using UnityEngine;
using Mirror;
using RoboBlast.Player.Interfaces;
using RoboBlast.Input;
using RoboBlast.Input.Interfaces;

namespace RoboBlast.Player
{
    [RequireComponent(typeof(IMovementController))]
    [RequireComponent(typeof(IHealthController))]
    [RequireComponent(typeof(IAttackController))]
    public class PlayerController : NetworkBehaviour
    {
        #region Start & Stop Callbacks

        /// <summary>
        /// This is invoked for NetworkBehaviour objects when they become active on the server.
        /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
        /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
        /// </summary>
        public override void OnStartServer() { }

        /// <summary>
        /// Invoked on the server when the object is unspawned
        /// <para>Useful for saving object data in persistant storage</para>
        /// </summary>
        public override void OnStopServer() { }

        /// <summary>
        /// Called on every NetworkBehaviour when it is activated on a client.
        /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
        /// </summary>
        public override void OnStartClient() { }

        /// <summary>
        /// This is invoked on clients when the server has caused this object to be destroyed.
        /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
        /// </summary>
        public override void OnStopClient() { }

        /// <summary>
        /// Called when the local player object has been set up.
        /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
        /// </summary>
        public override void OnStartLocalPlayer() { }

        /// <summary>
        /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
        /// <para>This is called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
        /// <para>When <see cref="NetworkIdentity.AssignClientAuthority"/> is called on the server, this will be called on the client that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnection parameter included, this will be called on the client that owns the object.</para>
        /// </summary>
        public override void OnStartAuthority() { }

        /// <summary>
        /// This is invoked on behaviours when authority is removed.
        /// <para>When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.</para>
        /// </summary>
        public override void OnStopAuthority() { }

        #endregion

        [SerializeField]
        private float _speedMultiplier;

        [SerializeField]
        private GameObject _graphics;

        private Renderer _renderer;

        private IMovementController _playerMovementController;
        private IHealthController _playerHealthController;
        private IAttackController _playerAttackController;

        private IMovementInput _playerMovementInput;
        private IAttackInput _playerAttackInput;

        private void Start()
        {
            if (_graphics != null)
                _renderer = GetComponentInChildren<Renderer>();

            if (hasAuthority == false)
                return;

            _playerMovementController = GetComponent<IMovementController>();
            _playerHealthController = GetComponent<IHealthController>();
            _playerAttackController = GetComponent<IAttackController>();

            _playerMovementInput = FindObjectOfType<JoystickInput>();
            _playerAttackInput = FindObjectOfType<AttackInput>();

            // Does this break SOLID principles? Probably...
            //(_playerHealthController as PlayerAttakc)?.AssignAuthority(connectionToClient);

            _playerMovementInput.OnPlayerMoveInput += _playerMovementController.Move;

            _playerAttackInput.OnPlayerPrimaryAttack += _playerAttackController.Attack;
            //_playerAttackInput.OnPlayerPrimaryAttack += _playerAttackController.AltAttack;

            _playerHealthController.OnDeath += CmdDie;
            
            _renderer.material.SetColor("_Color", Color.blue);
        }

        [Command]
        public void CmdDie()
        {
            Debug.Log("Goodbye cruel server world :(");

            NetworkServer.Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (hasAuthority == false)
                return;

            _playerAttackInput.OnPlayerPrimaryAttack -= _playerAttackController.Attack;
            //_playerAttackInput.OnPlayerPrimaryAttack -= _playerAttackController.AltAttack;

            _playerMovementInput.OnPlayerMoveInput -= _playerMovementController.Move;
            _playerHealthController.OnDeath -= CmdDie;
        }
    }
}