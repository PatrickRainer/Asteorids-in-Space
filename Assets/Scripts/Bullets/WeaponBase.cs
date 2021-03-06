using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Bullets
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] internal int damage = 1;
        [SerializeField] protected int speed = 5;

        [SerializeField] [AssetsOnly] [Required]
        GameObject destroyEffect;

        protected Rigidbody2D _rb;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

       protected virtual void OnDestroy()
        {
            if (destroyEffect == null) return;

            Instantiate((Object) destroyEffect, transform.position, quaternion.identity);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) return;
            if (other.CompareTag("PowerUp")) return;

            Destroy(gameObject);
        }

        protected virtual void Move()
        {
            var trf = transform;
            _rb.MovePosition(trf.position + trf.up * (Time.fixedDeltaTime * speed));
        }
    }
}