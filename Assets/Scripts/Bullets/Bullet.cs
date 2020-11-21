using System;
using CoreMechanics;
using UnityEngine;

namespace Bullets
{
    public class Bullet : StraightMover
    {
        [SerializeField] internal int damage = 1;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) return;

            Destroy(gameObject);
        }
    }
}