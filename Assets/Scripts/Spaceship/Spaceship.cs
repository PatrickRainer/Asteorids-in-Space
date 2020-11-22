using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoreMechanics;
using CoreMechanics.InputSystem;
using GameManagers;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceshipMechanics
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] int health = 10;
        [SerializeField] float velocity = 9;
        [SerializeField] float maxThrottle = 1;
        [SerializeField] [AssetsOnly] GameObject bullet;
        [SerializeField] Transform bulletAnchor;
        [SerializeField] float shootingInterval = 0.3f;

        [SerializeField] [AssetsOnly] [Required]
        GameObject destroyEffects;


        Vector3 _bulletStartPos;
        int _colObjID;
        float _currentShootInterval;
        GameManager _gameManager;
        InputListener _input;
        Rigidbody2D _rb;
        [Sirenix.OdinInspector.ReadOnly] public float currentThrottle = 0;

        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _input = FindObjectOfType<InputListener>();

            _rb = GetComponent<Rigidbody2D>();
            _bulletStartPos = bulletAnchor.transform.position;

            _input.ShootButtonPressed += ShootBullet;
            _input.UpButtonPressed += IncreaseThrottle;
            _input.DownButtonPressed += DecreaseThrottle;
            _input.MousePositionChanged += RotateToMousePosition;
        }

        void OnDisable()
        {
            _input.ShootButtonPressed -= ShootBullet;
            _input.UpButtonPressed -= IncreaseThrottle;
            _input.DownButtonPressed -= DecreaseThrottle;
            _input.MousePositionChanged -= RotateToMousePosition;
        }

        void RotateToMousePosition(Vector3 mousePos)
        {
            //Debug.Log("Rotate");

            var lookPos = mousePos;
            lookPos -= transform.position;
            var angle = (Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


        void Update()
        {
            _currentShootInterval -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            _rb.AddForce(transform.up * currentThrottle);
        }

        void OnDestroy()
        {
            if (GameManager.IsSceneUnloading) return;

            var go = Instantiate(destroyEffects);
            Destroy(go, 3);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var collidingObject = other.gameObject;

            if (collidingObject.GetInstanceID() == _colObjID) return; // Same Object collided twice

            _colObjID = collidingObject.GetInstanceID();

            if (collidingObject.layer == 11) return;


            //Debug.Log("Collision with SpaceShip");

            health -= 1;
            if (health <= 0)
            {
                //Debug.Log("Dead, Spaceship has no health anymore");
                FindObjectOfType<GameManager>()?.DecreaseLives();
                //Destroy(gameObject);
                //TODO: Flicker and be immutable for few seconds
                if (GameManager.CurrentLives <= 0) Destroy(gameObject);
            }
        }

        void IncreaseThrottle()
        {
            if (currentThrottle < maxThrottle)
            {
                currentThrottle = currentThrottle + Time.deltaTime;
                currentThrottle = Mathf.Clamp(currentThrottle, 0, maxThrottle);
            }
        }

        void DecreaseThrottle()
        {
            if (currentThrottle > 0)
            {
                currentThrottle = currentThrottle - Time.deltaTime;
                currentThrottle = Mathf.Clamp(currentThrottle, 0, maxThrottle);
            }
        }

        void ShootBullet()
        {
            if (_currentShootInterval <= 0)
            {
                _bulletStartPos = bulletAnchor.transform.position;
                Instantiate(bullet, _bulletStartPos, Quaternion.identity);
                _currentShootInterval = shootingInterval;
            }
        }
    }
}