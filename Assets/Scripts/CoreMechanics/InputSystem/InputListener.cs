using UnityEngine;
using UnityEngine.Events;

namespace CoreMechanics.InputSystem
{ //TODO: Should depend on input scheme (PC, Android, IOS, etc)
    public class InputListener : MonoBehaviour
    {
        MouseListener _mouseListener;
        
        // Movement - Rotating //

        public UnityAction<Vector3> MousePositionChanged = delegate {  };
        public UnityAction RotateButtonPressed = delegate { };
        public UnityAction RotateButtonReleased = delegate { };
        public UnityAction RotateButtonDown= delegate { };
        
        // Movement - Sliding //
        public UnityAction LeftButtonPressed = delegate { };
        public UnityAction RightButtonPressed = delegate { };
        
        // Movement - Throttling //
        public UnityAction BrakeButtonPressed = delegate { };
        public UnityAction ThrottleButtonPressed = delegate { };
        public UnityAction ThrottleButtonReleased = delegate { };
        
        // Shooting //
        public UnityAction ShootButtonPressed = delegate { };
        public UnityAction MissileButtonPressed = delegate { };
        public UnityAction RocketButtonPressed = delegate { };
        public UnityAction ClusterBombButtonPressed = delegate { };
        
        void Awake()
        {
            _mouseListener = gameObject.AddComponent<MouseListener>();
            _mouseListener.MousePositionChanged += delegate(Vector3 newMousePos) {  MousePositionChanged.Invoke(newMousePos);};
        }

        void Update()
        {
            if (Input.GetMouseButton(0)) ShootButtonPressed.Invoke();
            

            if (Input.GetKey(KeyCode.A)) LeftButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.D)) RightButtonPressed.Invoke();
            if (Input.GetMouseButton(1)) ThrottleButtonPressed.Invoke();
            if (Input.GetMouseButtonUp(1)) ThrottleButtonReleased.Invoke();
            if (Input.GetKey(KeyCode.S)) BrakeButtonPressed.Invoke();
            if (Input.GetKey(KeyCode.LeftAlt)) RotateButtonPressed.Invoke();
            if (Input.GetKeyUp(KeyCode.LeftAlt)) RotateButtonReleased.Invoke();
            if (Input.GetKeyDown(KeyCode.LeftAlt)) RotateButtonDown.Invoke();

            
            if(Input.GetKey(KeyCode.Alpha1)) MissileButtonPressed.Invoke();
            if(Input.GetKey(KeyCode.Alpha2)) RocketButtonPressed.Invoke();
            if(Input.GetKey(KeyCode.Alpha3)) ClusterBombButtonPressed.Invoke();
        }
    }
}