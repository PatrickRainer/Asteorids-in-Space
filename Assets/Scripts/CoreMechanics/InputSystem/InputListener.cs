﻿using UnityEngine;
using UnityEngine.Events;

namespace CoreMechanics.InputSystem
{ //TODO: Should depend on input scheme
    public class InputListener : MonoBehaviour
    {
        MouseListener _mouseListener;
        public UnityAction BrakeButtonPressed = delegate { };
        public UnityAction LeftButtonPressed = delegate { };

        public UnityAction<Vector3> MousePositionChanged = delegate(Vector3 arg0) {  };

        public UnityAction RightButtonPressed = delegate { };

        public UnityAction ShootButtonPressed = delegate
        {
            /*Debug.Log("Shoot Key");*/
        };

        public UnityAction ThrottleButtonPressed = delegate { };
        public UnityAction ThrottleButtonReleased = delegate { };

        void Awake()
        {
            _mouseListener = gameObject.AddComponent<MouseListener>();
            _mouseListener.MousePositionChanged += delegate(Vector3 newMousePos) {  MousePositionChanged.Invoke(newMousePos);};
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space)) ShootButtonPressed.Invoke();

            if (Input.GetKey(KeyCode.A)) LeftButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.D)) RightButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.W)) ThrottleButtonPressed.Invoke();
            if (Input.GetKeyUp(KeyCode.W)) ThrottleButtonReleased.Invoke();
            if (Input.GetKey(KeyCode.S)) BrakeButtonPressed.Invoke();
        }
    }
}