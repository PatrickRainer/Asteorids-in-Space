using System;
using CoreMechanics;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : FloatingObject
    {
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

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) return;

            Destroy(gameObject);
        }
    }
}