using CoreMechanics.InputSystem;
using GameManagers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceshipMechanics
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] int health = 10;
        [SerializeField] float maxThrottle = 3;
        [SerializeField] [AssetsOnly] GameObject bullet;
        [SerializeField] Transform bulletAnchor;
        [SerializeField] float shootingInterval = 0.3f;

        [SerializeField] float rotationDeadZone = 0.2f;

        [SerializeField] float rotationSpeed = 200f;

        [SerializeField] [AssetsOnly] [Required]
        GameObject destroyEffects;

        Vector3 _bulletStartPos;
        int _colObjID;
        float _currentShootInterval;
        GameManager _gameManager;
        InputListener _input;
        Rigidbody2D _rb;
        [ReadOnly] float currentThrottle = 0;

        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _input = FindObjectOfType<InputListener>();
            FindObjectOfType<MouseListener>();

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
            if (!MouseListener.IsMouseOnScreen()) return;

            var relativeMousePos = transform.InverseTransformPoint(mousePos);
            var isMouseInDeadZone =
                relativeMousePos.x > 0f - rotationDeadZone && relativeMousePos.x < 0f + rotationDeadZone;

            if (isMouseInDeadZone)
            {
                return;
            }
            else if (relativeMousePos.x < 0f)
            {
                _rb.MoveRotation(_rb.rotation + rotationSpeed * Time.fixedDeltaTime);
            }
            else if (relativeMousePos.x > 0f)
            {
                _rb.MoveRotation(_rb.rotation + -rotationSpeed * Time.fixedDeltaTime);
            }
        }


        void Update()
        {
            _currentShootInterval -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            _rb.MovePosition(transform.position + transform.up * (Time.fixedDeltaTime * currentThrottle));
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
                Instantiate(bullet, _bulletStartPos, transform.rotation);
                _currentShootInterval = shootingInterval;
            }
        }

        public float GetCurrentThrottlePercentage()
        {
            return 100 / maxThrottle * currentThrottle / 100;
        }
    }
}