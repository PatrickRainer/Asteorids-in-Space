using System;
using UnityEngine;

namespace CoreMechanics
{
    public class CreateScreenEdges : MonoBehaviour
    {
        Camera _camera;
        float _screenHeight;
        float _screenWidth;


        void Start()
        {
            _camera  = Camera.main;
            if (_camera == null) return;

            _screenHeight = _camera.orthographicSize * 2;
            _screenWidth = _screenHeight * Screen.width/ Screen.height;

            CreateLeftWall();
            CreateRightWall();
            CreateTopWall();
            CreateBottomWall();
        }

        void CreateLeftWall()
        {
            var go = new GameObject("Left Wall") {layer = 11};
            go.transform.position = Positioner.GetScreenLeftMiddle() - new Vector3(.1f, 0, 0);
            go.transform.localScale = new Vector3(.1f, _screenHeight, 1);

            var rb = go.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            var cl = go.AddComponent<BoxCollider2D>();
        }
        
        void CreateRightWall()
        {
            var go = new GameObject("Right Wall") {layer = 11};
            go.transform.position = Positioner.GetScreenRightMiddle() + new Vector3(.1f, 0, 0);
            go.transform.localScale = new Vector3(.1f, _screenHeight, 1);

            var rb = go.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            var cl = go.AddComponent<BoxCollider2D>();
        }
        
        void CreateTopWall()
        {
            var go = new GameObject("Top Wall") {layer = 11};
            go.transform.position = Positioner.GetScreenTopMiddle() + new Vector3(0, .1f, 0);
            go.transform.localScale = new Vector3(_screenWidth, .1f, 1);

            var rb = go.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            var cl = go.AddComponent<BoxCollider2D>();
        }
        
        void CreateBottomWall()
        {
            var go = new GameObject("Bottom Wall") {layer = 11};
            go.transform.position = Positioner.GetScreenBottomMiddle() - new Vector3(0, .1f, 0);
            go.transform.localScale = new Vector3(_screenWidth, .1f, 1);

            var rb = go.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            var cl = go.AddComponent<BoxCollider2D>();
        }
    }
}