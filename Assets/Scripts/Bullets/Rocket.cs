using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rocket : WeaponBase
    {
        //BUG: Moves into the wrong direction
    }
}