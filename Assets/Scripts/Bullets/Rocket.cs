using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Bullets
{
    public class Rocket : WeaponBase
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == gameObject.layer) return;
            
            Destroy(gameObject);
        }
    }
}