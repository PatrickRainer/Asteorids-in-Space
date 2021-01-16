using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Bullets;
using CoreMechanics.InputSystem;
using GameManagers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Spaceships
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] int health = 10;
        [SerializeField] float maxThrottle = 3;
        [SerializeField] float throttleSensibility = 1;
        [SerializeField] float throttleDecreaseSensibility = 3;
        [SerializeField] float shootingInterval = 0.3f;
        [SerializeField] float rotationDeadZone = 0.2f;
        [SerializeField] float rotationSpeed = 200f;

        [SerializeField] [Required] [SceneObjectsOnly]
        Transform bulletAnchorMiddle;

        [SerializeField] [Required] [SceneObjectsOnly]
        Transform bulletAnchorLeft;

        [SerializeField] [Required] [SceneObjectsOnly]
        Transform bulletAnchorRight;


        [SerializeField] [AssetsOnly] [Required]
        GameObject bullet;

        [SerializeField] [AssetsOnly] [Required]
        GameObject destroyEffects;

        int _colObjID;
        float _currentShootInterval;
        [ShowInInspector] [ReadOnly] float _currentThrottle;
        GameManager _gameManager;
        InputListener _input;
        bool _isRotating = true;
        bool _isThrottling;
        [ShowInInspector, ReadOnly] readonly List<GameObject> _loadedRockets = new List<GameObject>();
        Rigidbody2D _rb;
        internal int ActiveCannons = 1;


        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _input = FindObjectOfType<InputListener>();
            FindObjectOfType<MouseListener>();

            _rb = GetComponent<Rigidbody2D>();

            _input.ShootButtonPressed += ShootBullet;
            _input.ThrottleButtonPressed += IncreaseThrottle;
            _input.ThrottleButtonReleased += () => { _isThrottling = false; };
            _input.BrakeButtonPressed += DecreaseThrottle;
            _input.MousePositionChanged += RotateToMousePosition;
            _input.RotateButtonDown += delegate { _isRotating = !_isRotating; };
        }

        void Update()
        {
            _currentShootInterval -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            _rb.MovePosition(transform.position + transform.up * (Time.fixedDeltaTime * _currentThrottle));

            if (!_isThrottling) DecreaseThrottle();
        }

        void OnEnable()
        {
        }

        void OnDisable()
        {
            _input.ShootButtonPressed -= ShootBullet;
            _input.ThrottleButtonPressed -= IncreaseThrottle;
            _input.BrakeButtonPressed -= DecreaseThrottle;
            _input.MousePositionChanged -= RotateToMousePosition;
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

            if (collidingObject.layer != LayerMask.NameToLayer("EnemyObjects")) return;
            // if (collidingObject.layer == 11) return;
            // if (collidingObject.CompareTag("PowerUp")) return;


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

        void RotateToMousePosition(Vector3 mousePos)
        {
            if (!_isRotating) return;

            if (!MouseListener.IsMouseOnScreen()) return;

            var relativeMousePos = transform.InverseTransformPoint(mousePos);
            var isMouseInDeadZone =
                relativeMousePos.x > 0f - rotationDeadZone && relativeMousePos.x < 0f + rotationDeadZone;

            if (isMouseInDeadZone)
                return;
            if (relativeMousePos.x < 0f)
                _rb.MoveRotation(_rb.rotation + rotationSpeed * Time.fixedDeltaTime);
            else if (relativeMousePos.x > 0f) _rb.MoveRotation(_rb.rotation + -rotationSpeed * Time.fixedDeltaTime);
        }

        void IncreaseThrottle()
        {
            _isThrottling = true;
            if (_currentThrottle < maxThrottle)
            {
                _currentThrottle = _currentThrottle + Time.deltaTime * throttleSensibility;
                _currentThrottle = Mathf.Clamp(_currentThrottle, 0, maxThrottle);
            }
        }

        void DecreaseThrottle()
        {
            if (_currentThrottle > 0)
            {
                _currentThrottle = _currentThrottle - Time.deltaTime * throttleDecreaseSensibility;
                _currentThrottle = Mathf.Clamp(_currentThrottle, 0, maxThrottle);
            }
        }

        void ShootBullet()
        {
            var bulletMiddlePos = bulletAnchorMiddle.transform.position;
            var bulletLeftPos = bulletAnchorLeft.transform.position;
            var bulletRightPos = bulletAnchorRight.transform.position;


            if (_currentShootInterval <= 0)
                switch (ActiveCannons)
                {
                    case 1:
                        Instantiate(bullet, bulletMiddlePos, transform.rotation);
                        _currentShootInterval = shootingInterval;
                        break;
                    case 2:
                        Instantiate(bullet, bulletLeftPos, transform.rotation);
                        Instantiate(bullet, bulletRightPos, transform.rotation);
                        _currentShootInterval = shootingInterval;
                        break;
                    case 3:
                        Instantiate(bullet, bulletMiddlePos, transform.rotation);
                        Instantiate(bullet, bulletLeftPos, transform.rotation);
                        Instantiate(bullet, bulletRightPos, transform.rotation);
                        _currentShootInterval = shootingInterval;
                        break;
                }
        }

        public float GetCurrentThrottlePercentage()
        {
            return 100 / maxThrottle * _currentThrottle / 100;
        }

        [Button]
        public void ShootNextRocket<T>() where  T: WeaponBase // TODO: How to shoot the rocket?
        {
            var lastRocketIndex = _loadedRockets.FindLastIndex(o => o.GetComponent<T>());
            if (lastRocketIndex <=-1) return;
            
            //if (_loadedRockets.Count(o => GetComponent<T>()) == 0) return;

            Instantiate(_loadedRockets[lastRocketIndex], bulletAnchorMiddle.position, bulletAnchorMiddle.rotation);
            _loadedRockets.RemoveAt(lastRocketIndex);
        }

        [Button]
        public void AddRocketToLoad(GameObject rocket)
        {
            _loadedRockets.Add(rocket);
        }

        public int GetRocketLoadCount()
        {
            var count = _loadedRockets.Count(o => o.GetComponent<Rocket>() != null);
            return count;
        }

        public int GetMissileLoadCount()
        {
            var count = _loadedRockets.Count(o => o.GetComponent<Missile>() != null);
            return count;
        }

        public int GetClusterBombLoadCount()
        {
            var count = _loadedRockets.Count(o => o.GetComponent<ClusterBomb>() != null);
            return count;
        }
    }
}