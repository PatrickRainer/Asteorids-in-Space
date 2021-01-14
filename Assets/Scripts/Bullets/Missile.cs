using UnityEngine;

namespace Bullets
{
    public class Missile : WeaponBase
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            throw new System.NotImplementedException();
        }
    }
}