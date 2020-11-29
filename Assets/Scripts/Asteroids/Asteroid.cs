using System.Collections.Generic;
using Bullets;
using GameManagers;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] int health = 2;
        [SerializeField] int scorePoints = 1;
        [SerializeField] int rotationSpeed = 100;
        [SerializeField, Range(0,100)] int chanceToDropPowerUp;
        [SerializeField] [AssetsOnly] GameObject destroyPrefab;
        [SerializeField] public List<GameObject> powerUps = new List<GameObject>();
        Vector2 _originLookDir;
        PowerUpDropper _powerUpDropper;
        Rigidbody2D _rb;

        Rotator _rotator;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _powerUpDropper = gameObject.AddComponent<PowerUpDropper>();
            _powerUpDropper.Init(this);
        }

        protected void Start()
        {
            _rotator = new Rotator(GetComponent<Rigidbody2D>(), rotationSpeed);

            // Look to middle of the screen
            var dir = Vector3.zero - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            _originLookDir = dir.normalized;
        }

        protected void FixedUpdate()
        {
            _rotator.Rotate();

            Move();
        }

        void OnDestroy()
        {
            if (GameManager.IsSceneUnloading) return;

            _powerUpDropper.DropRandomPowerUp(this);

            if (!(destroyPrefab is null)) Instantiate(destroyPrefab, transform.position, quaternion.identity);

            GameManager.CurrentScore += scorePoints;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Triggered Asteroid collider");

            var colObj = other.gameObject;

            if (other.CompareTag("Player")) health -= 1;

            if (colObj.layer == 9) //Bullet
            {
                var bullet = other.GetComponent<Bullet>();
                health -= bullet.damage;
            }

            if (health <= 0) Destroy(gameObject);
        }

        void Move()
        {
            Vector2 dir = (Vector3.zero - transform.position).normalized;
            _rb.MovePosition(_rb.position + _originLookDir * Time.fixedDeltaTime);
        }
    }
}