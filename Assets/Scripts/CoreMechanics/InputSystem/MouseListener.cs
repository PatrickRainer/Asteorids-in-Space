using UnityEngine;
using UnityEngine.Events;

namespace CoreMechanics.InputSystem
{
    public class MouseListener : MonoBehaviour
    {
        Vector3 _currentMousePosition;
        Camera _mainCamera;
        Vector3 _worldMousePosition;

        public UnityAction<Vector3> MousePositionChanged = delegate { };

        void Awake()
        {
            _currentMousePosition = Input.mousePosition;
        }

        void Start()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            _worldMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -10);

            if (Input.mousePosition != _currentMousePosition) MousePositionChanged.Invoke(_worldMousePosition);
            //Debug.Log(MousePosition.ToString());
        }
    }
}