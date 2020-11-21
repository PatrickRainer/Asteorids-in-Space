using Bullets;
using CoreMechanics;
using GameManagers;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : StraightMover
    {
        [SerializeField] int health = 2;
        [SerializeField] int scorePoints = 1;
        [SerializeField] int rotationSpeed = 100;
        [SerializeField] [AssetsOnly] GameObject destroyPrefab;

        Rotator _rotator;


        protected override void Start()
        {
            base.Start();
            _rotator = new Rotator(GetComponent<Rigidbody2D>(), rotationSpeed);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _rotator.Rotate();
        }

        void OnDestroy()
        {
            if (GameManager.IsSceneUnloading) return;

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
    }
}