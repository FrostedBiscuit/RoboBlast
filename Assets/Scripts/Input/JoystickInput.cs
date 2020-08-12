using System;
using UnityEngine;
using UnityEngine.EventSystems;
using RoboBlast.Input.Interfaces;

namespace RoboBlast.Input
{
    public class JoystickInput : Joystick, IMovementInput
    {
        public event Action<Vector2> OnPlayerMoveInput;

        private bool _broadcastEvent = false;

        public override void OnPointerDown(PointerEventData eventData)
        {
            _broadcastEvent = true;

            base.OnPointerDown(eventData);
        }

        private void Update()
        {
            if (_broadcastEvent)
                OnPlayerMoveInput?.Invoke(Direction);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _broadcastEvent = false;
        }
    }
}