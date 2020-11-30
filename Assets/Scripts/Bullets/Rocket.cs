using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Bullets
{
    public class Rocket : WeaponBase
    {
        [SerializeField, AssetsOnly, Required] GameObject destroyEffect;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == gameObject.layer) return;
            
            Destroy(gameObject);
        }

        void OnDestroy()
        {
            Instantiate(destroyEffect, transform.position, quaternion.identity);
        }
    }
}