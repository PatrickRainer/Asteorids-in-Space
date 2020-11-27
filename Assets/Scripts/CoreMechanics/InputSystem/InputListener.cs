using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        public UnityAction RotateButtonPressed = delegate { };
        public UnityAction RotateButtonReleased = delegate { };
        public UnityAction RotateButtonDown= delegate { };

        void Awake()
        {
            _mouseListener = gameObject.AddComponent<MouseListener>();
            _mouseListener.MousePositionChanged += delegate(Vector3 newMousePos) {  MousePositionChanged.Invoke(newMousePos);};
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space)) ShootButtonPressed.Invoke();
            if (Input.GetMouseButton(0)) ShootButtonPressed.Invoke();

            if (Input.GetKey(KeyCode.A)) LeftButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.D)) RightButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.W)) ThrottleButtonPressed.Invoke();
            if (Input.GetKeyUp(KeyCode.W)) ThrottleButtonReleased.Invoke();
            if (Input.GetKey(KeyCode.S)) BrakeButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.LeftAlt)) RotateButtonPressed.Invoke();
            if (Input.GetKeyUp(KeyCode.LeftAlt)) RotateButtonReleased.Invoke();
            if (Input.GetKeyDown(KeyCode.LeftAlt)) RotateButtonDown.Invoke();
        }
    }
}