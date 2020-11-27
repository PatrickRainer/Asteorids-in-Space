using System;
using UnityEngine;

namespace CoreMechanics
{
    public class Positioner : MonoBehaviour
    {
        static Camera _mainCamera;

        void Awake()
        {
            _mainCamera = Camera.main;
        }

        void Start()
        {
            
            if (_mainCamera == null) gameObject.SetActive(false);
        }

        public static Vector3 GetScreenTopMiddle()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((Screen.width / 2f), Screen.height, 0));
            return result;
        }

        public static Vector3 GetScreenRightMiddle()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((Screen.width), (Screen.height / 2f), 0));
            return result;
        }

        public static Vector3 GetScreenLeftMiddle()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((0), (Screen.height / 2f), 0));
            return result;
        }

        public static Vector3 GetScreenBottomMiddle()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((Screen.width / 2f), (0), 0));
            return result;
        }

        public static Vector3 GetScreenBottomLeft()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((0), (0), 0));
            return result;
        }

        public static Vector3 GetScreenBottomRight()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((Screen.width), (0), 0));
            return result;
        }

        public static Vector3 GetScreenTopLeft()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((0), (Screen.height), 0));
            return result;
        }

        static Vector3 GetScreenTopRight()
        {
            var result = _mainCamera.ScreenToWorldPoint(new Vector3((Screen.width), (Screen.height), 0));
            return result;
        }

        public static float GetDistanceToScreenEdge()
        {
            float distance = 0;
            
            Camera mainCam = Camera.main;
            if (mainCam == null) return distance;

            Vector2 diagonalScreenEdge = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));

            distance = Vector2.Distance(diagonalScreenEdge, Vector2.zero);
            return distance;
        }
    }
}