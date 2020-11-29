using UnityEngine;

namespace CoreMechanics
{
    public abstract class FloatingObject : MonoBehaviour
    {
        [SerializeField] protected int health = 2;
        [SerializeField] protected int scorePoints = 1;
        [SerializeField] internal int damage = 1;
        [SerializeField] internal bool canHitPlayerShip;
        [SerializeField] internal bool canHitShield;
    }
}