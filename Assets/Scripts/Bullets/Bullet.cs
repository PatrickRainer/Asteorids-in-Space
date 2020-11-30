using System;
using CoreMechanics;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : WeaponBase
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) return;
            if (other.CompareTag("PowerUp")) return;

            Destroy(gameObject);
        }
    }
}