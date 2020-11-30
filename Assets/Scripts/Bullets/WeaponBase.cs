using UnityEngine;

namespace Bullets
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] internal int damage = 1;
        [SerializeField] int speed = 5;
        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            MoveForward();
        }

        void MoveForward()
        {
            var trf = transform;
            _rb.MovePosition(trf.position + trf.up * (Time.fixedDeltaTime * speed));
        }

        protected abstract void OnTriggerEnter2D(Collider2D other);
    }
}