using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Missile : WeaponBase
    {
        //TODO: Currently the missile does the same like the rocket.
        // May be I could implement a aiming pointer which the missile is aiming for
    }
}